using Azure.Core;
using CropMeasureEligibility.Editor.Common;
using CropMeasureEligibility.Editor.Enums;
using CropMeasureEligibility.Editor.Infrastructure.Persistance;
using CropMeasureEligibility.Editor.Models;
using CropMeasureEligibility.Editor.Models.ActionContext;
using CropMeasureEligibility.Editor.Models.ListD;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
using System.Threading.Channels;

namespace CropMeasureEligibility.Editor.Service
{
	internal static class Service
	{

		public static readonly List<int> AlwaysEnabledDeclaration = new() {
			(int)CommonRequestDocumentDeclarationEnum.Bovidae,
			(int)CommonRequestDocumentDeclarationEnum.DairyCow,
			(int)CommonRequestDocumentDeclarationEnum.CowsInMilkProduction,
			(int)CommonRequestDocumentDeclarationEnum.FirstBreedCowsPayment
		};

		internal static async Task UpdateJsonToDb()
		{
			using var context = new EditorDbContext();

			List<FarmSourceCropMeasuresEligibility> farmSourceCropMeasuresEligibilityes =
								await context.FarmSourceCropMeasuresEligibilityes.ToListAsync();

			List<FarmDestinationCropMeasuresEligibility> farmDestinationCropMeasuresEligibility = new List<FarmDestinationCropMeasuresEligibility>();



			foreach (FarmSourceCropMeasuresEligibility farmSourceCropMeasuresEligibility in farmSourceCropMeasuresEligibilityes)
			{
				CropMeasureEligibilityDto cropMeasureEligibility = Helpers.DeserializeFromJsonString<CropMeasureEligibilityDto>(farmSourceCropMeasuresEligibility.CropMeasureEligibility);

				List<ActionContextIdIdentifier> actionContextIdIdentifiers = await context.ActionContextIdIdentifiers.Where(x => x.ActionContextId == farmSourceCropMeasuresEligibility.ActionContextId).ToListAsync();

				List<Guid> existingKeys = new List<Guid>();

				Dictionary<Guid, List<ArkodCropMeasureEligibilityDto>> arkodCropMeasureEligibilities = cropMeasureEligibility.ArkodCropMeasureEligibilities;

				foreach (var entry in arkodCropMeasureEligibilities)
				{
					Guid key = entry.Key;
					List<ArkodCropMeasureEligibilityDto> values = entry.Value;

					existingKeys.AddRange(values.Where(x => x.MeasureDefinitionId == Convert.ToInt16(ItemDataEnum.MeasureDefinitionId)).Select(x => x.Identifier));
				}

				List<Guid> missingKeys = actionContextIdIdentifiers
											.Where(a => !existingKeys.Contains(a.Identifier))
											.Select(a => a.Identifier)
											.ToList();

				if (missingKeys.Any())
				{
					#region ArkodCropMeasureEligibilities

					foreach (Guid missingKey in missingKeys)
					{
						var entry = cropMeasureEligibility.ArkodCropMeasureEligibilities.FirstOrDefault(x => x.Key == missingKey);

						if (entry.Value != null)
						{
							entry.Value.Add(new ArkodCropMeasureEligibilityDto
							{
								Identifier = entry.Key,
								MeasureDefinitionId = Convert.ToInt16(ItemDataEnum.MeasureDefinitionId),
								CropMeasureGroupId = Convert.ToInt16(ItemDataEnum.CropMeasureGroupId),
								CropGroupId = Convert.ToInt16(ItemDataEnum.CropGroupId),
								IsVgo = false,
								ObligationYearEnd = null
							});
						}
						else
						{
							cropMeasureEligibility.ArkodCropMeasureEligibilities
								.Add(missingKey, new List<ArkodCropMeasureEligibilityDto>
								{
									new ArkodCropMeasureEligibilityDto
									{
										Identifier = missingKey,
										MeasureDefinitionId = Convert.ToInt16(ItemDataEnum.MeasureDefinitionId),
										CropMeasureGroupId = Convert.ToInt16(ItemDataEnum.CropMeasureGroupId),
										CropGroupId = Convert.ToInt16(ItemDataEnum.CropGroupId),
										IsVgo = false,
										ObligationYearEnd = null
									}
								});
						}
					}

					#endregion
				}


				#region ItemMeasures

				Dictionary<Guid, Dictionary<int, bool?>> itemMeasures = cropMeasureEligibility.ItemMeasures;

				//update existing measures
				foreach (var entry in itemMeasures)
				{
					Guid key = entry.Key;
					Dictionary<int, bool?> values = entry.Value;

					if (!actionContextIdIdentifiers.Where(x => x.Identifier == key).Any())
						continue;

					var falseValues = values.Where(x => x.Key == Convert.ToInt16(ItemDataEnum.MeasureDefinitionId) && x.Value == false).ToList();

					foreach (var falseValue in falseValues)
					{
						values[falseValue.Key] = true;
					}
				}

				IEnumerable<Guid> itemMeasuresGuids = itemMeasures.Keys.ToList();

				var missingItemMeasures = actionContextIdIdentifiers
											.Where(a => !itemMeasuresGuids.Contains(a.Identifier))
											.Select(a => a.Identifier)
											.ToList();

				if (missingItemMeasures.Any())
				{
					//add missing measures
					foreach (Guid missingItemMeasure in missingItemMeasures)
					{
						Dictionary<int, bool?> newValue = new Dictionary<int, bool?>
						{
							{ Convert.ToInt16(ItemDataEnum.MeasureDefinitionId), true }
						};

						cropMeasureEligibility.ItemMeasures.Add(missingItemMeasure, newValue);
					}

				}
				#endregion

				#region EligibleDeclarations

				Dictionary<int, bool> eligibleDeclarations = cropMeasureEligibility.EligibleDeclarations;

				if (eligibleDeclarations.Where(x => x.Key == Convert.ToInt16(ItemDataEnum.MeasureId)).Any())
				{
					if (eligibleDeclarations.Where(x => x.Key == Convert.ToInt16(ItemDataEnum.MeasureId) && x.Value == false).Any())
						eligibleDeclarations[Convert.ToInt16(ItemDataEnum.MeasureId)] = true;
				}
				else
				{
					eligibleDeclarations.Add(Convert.ToInt16(ItemDataEnum.MeasureId), true);
				}
				#endregion

				farmDestinationCropMeasuresEligibility.Add(new FarmDestinationCropMeasuresEligibility
				{
					FarmId = farmSourceCropMeasuresEligibility.FarmId,
					ActionContextId = farmSourceCropMeasuresEligibility.ActionContextId,
					CropMeasureEligibility = Helpers.SerializeToJsonString<CropMeasureEligibilityDto>(cropMeasureEligibility)
				});

				await context.FarmDestinationCropMeasuresEligibilityes.AddRangeAsync(farmDestinationCropMeasuresEligibility);
				await context.SaveChangesAsync();

			}
		}

		internal static async Task UpdateJsonToFile()
		{
			#region Define file paths
			string solutionDirectory = Helpers.GetProjectDirectory();
			string sourceFile = Path.Combine(solutionDirectory, "sourceData.txt");
			string destinationFile = Path.Combine(solutionDirectory, "destinationData.txt");
			#endregion

			#region Get JSON

			//read from txt file
			CropMeasureEligibilityDto cropMeasureEligibilityDto = await Helpers.DeserializeFromJsonFileAsync<CropMeasureEligibilityDto>(sourceFile);

			Dictionary<Guid, List<ArkodCropMeasureEligibilityDto>> arkodCropMeasureEligibilities = cropMeasureEligibilityDto.ArkodCropMeasureEligibilities;

			List<Guid> identifiers = new List<Guid>();

			foreach (var entry in arkodCropMeasureEligibilities)
			{
				Guid key = entry.Key;
				List<ArkodCropMeasureEligibilityDto> values = entry.Value;

				if (values.Where(x => x.MeasureDefinitionId == 446).Any())
					identifiers.Add(values.Where(x => x.MeasureDefinitionId == 446).First().Identifier);
			}

			var stop = 0;

			#endregion

			#region Save new JSON

			//save to TXT file
			//await Helpers.SerializeToJsonFileAsync(destinationFile, cropMeasureEligibilityDto);


			#endregion
		}

		internal static async Task GeneratePdfFile(string submissionId, string farmId)
		{

			string url = "http://testisa15/demeter/webservice/Subsidy/CommonClaim.asmx/StartSubmissionFileAsyncTask";

			using (HttpClient client = new HttpClient())
			{

				var content = new StringContent($"submissionId={submissionId}&farmId={farmId}", Encoding.UTF8, "application/x-www-form-urlencoded");

				try
				{

					HttpResponseMessage response = await client.PostAsync(url, content);


					if (response.IsSuccessStatusCode)
					{
						string responseContent = await response.Content.ReadAsStringAsync();
						Console.WriteLine("Response from server:");
						Console.WriteLine(responseContent);
					}
					else
					{
						Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"An error occurred: {ex.Message}");
				}
			}

		}

		internal static async Task UpdateJsonListD()
		{
			using var context = new EditorDbContext();

			List<ActionContextLivestockRequestItem> actionContextLivestockRequestItems = await context.ActionContextLivestockRequestItems
				.Where(x => x.RequestDocumentTypeId == 4 && String.IsNullOrEmpty(x.LivestockRequestItemsAfterUpdate))
				.ToListAsync();

			foreach (ActionContextLivestockRequestItem livestockRequestItem in actionContextLivestockRequestItems)
			{
				if (!String.IsNullOrEmpty(livestockRequestItem.LivestockRequestItemsAfterUpdate))
					continue;

				if (!String.IsNullOrEmpty(livestockRequestItem.LivestockRequestItems))
				{
					LivestockRequestItemChanges changes = Helpers.DeserializeFromJsonString<LivestockRequestItemChanges>(livestockRequestItem.LivestockRequestItems);

					IEnumerable<LivestockRequestItemGroupedDto> groupedDtos = changes.Changes.Values
						.GroupBy(x => x.Jibg)
						.ToDictionary(x => x.Key, y => y.ToList())
						.Select(x => new LivestockRequestItemGroupedDto
						{
							Jibg = x.Key,
							LivestockRequestItems = x.Value
						});

					if (groupedDtos != null && groupedDtos.Any())
					{
						if (!changes.Declarations.Keys.ToList().Any(x => x == 14))
							changes.Declarations.Add(14, false);

						if (!changes.Declarations.Keys.ToList().Any(x => x == 89))
							changes.Declarations.Add(89, false);

						foreach (LivestockRequestItemGroupedDto item in groupedDtos)
						{
							LivestockRequestItemGroupedDto livestockRequestItemGroup = new LivestockRequestItemGroupedDto
							{
								Jibg = item.Jibg,
								LivestockRequestItems = item.LivestockRequestItems
							};

							List<LivestockRequestItemDto> livestockRequestItemsWithoutAnimylTypeId = livestockRequestItemGroup.LivestockRequestItems.Where(x => x.AnimalTypeId == null).ToList();

							if (livestockRequestItemsWithoutAnimylTypeId.Any())
							{
								List<DcanimalBreed> dcanimalBreeds = await context.DcanimalBreeds.ToListAsync();

								foreach (var livestockItem in livestockRequestItemsWithoutAnimylTypeId)
								{
									livestockItem.AnimalTypeId = dcanimalBreeds.FirstOrDefault(x => x.Id == livestockItem.AnimalBreedId)?.AnimalTypeId;
								}
							}

							IList<int> vgoAnimalBreedObligation = await context.VgoLivestockProtectedObligations.AsNoTracking()
								.Where(x => x.FarmId == livestockRequestItem.FarmId && x.EndYear >= livestockRequestItem.ActionContextYear && x.Active == true)
								.TagWith("CommonLivestockService.GetLivestockRequestGroupedItems")
								.Select(x => x.AnimalBreedId)
								.ToListAsync();

							if (vgoAnimalBreedObligation.Any())
							{
								List<LivestockRequestItemDto> filteredLivestockRequestItem = livestockRequestItemGroup.LivestockRequestItems
									.Where(x => vgoAnimalBreedObligation.Contains((int)x.AnimalBreedId))
									.ToList();

								if (filteredLivestockRequestItem.Any())
								{
									foreach (var livestockItem in filteredLivestockRequestItem)
									{
										if (livestockItem.MeasureItems.Count() < 2 && !livestockItem.MeasureItems.Any(x => x.MeasureDefinitionId == 509))
										{
											livestockItem.IsVgoChecked = null;

											LivestockRequestItemMeasureDto measureItem = new LivestockRequestItemMeasureDto()
											{
												LivestockId = livestockItem.LivestockId,
												MeasureDefinitionId = 509,
												MeasureId = 20,
												MeasureCode = "09",
												IsSeparatedMeasure = false,
												IsChecked = null,
												IsChangedInRequest = null
											};

											livestockItem.MeasureItems.Add(measureItem);
										}
									}
								}
							}
						}
					}

					changes.ChangesInternal = changes.Changes.ToDictionary((i) => i.Key, (i) => i.Value);

					livestockRequestItem.LivestockRequestItemsAfterUpdate = Helpers.SerializeToJsonString<LivestockRequestItemChanges>(changes);
				}
			}

			await context.SaveChangesAsync();
		}

		internal static async Task UpdateJsonListC()
		{

		}

		internal static async Task CreateActionContext(RequestDocumentTypeEnum requestDocumentType)
		{
			using var context = new EditorDbContext();

			List<ActionContextLivestockRequestItem> actionContextLivestockRequestItems = await context.ActionContextLivestockRequestItems
				.Where(x => x.RequestDocumentTypeId == (int)requestDocumentType && String.IsNullOrEmpty(x.LivestockRequestItemsAfterUpdate))
				.ToListAsync();

			foreach (ActionContextLivestockRequestItem livestockRequestItem in actionContextLivestockRequestItems)
			{
				if (!String.IsNullOrEmpty(livestockRequestItem.LivestockRequestItemsAfterUpdate))
					continue;

				// Kreiranje LivestockDictionary
				LivestockActionContext livestockActionContext = await CreateLivestockActionContext(livestockRequestItem, requestDocumentType);

				// Spramanje LivestockDictionary
				livestockRequestItem.LivestockDictionaryAfterUpdate = Helpers.SerializeToJsonString<LivestockActionContext>(livestockActionContext);

				// Kreiranje LivestockRequestItems
				LivestockRequestItemChanges livestockChanges = await CreateLivestockRequestItemChanges(livestockActionContext, livestockRequestItem.ActionContextYear, livestockRequestItem.FarmId, requestDocumentType);

				//TODO: Provjerit dali je potrebno izmjeniti nesto prije spramanja

				//	if (!String.IsNullOrEmpty(livestockRequestItem.LivestockRequestItems))
				//	{
				//		LivestockRequestItemChanges changes = Helpers.DeserializeFromJsonString<LivestockRequestItemChanges>(livestockRequestItem.LivestockRequestItems);

				//		IEnumerable<LivestockRequestItemGroupedDto> groupedDtos = changes.Changes.Values
				//			.GroupBy(x => x.Jibg)
				//			.ToDictionary(x => x.Key, y => y.ToList())
				//			.Select(x => new LivestockRequestItemGroupedDto
				//			{
				//				Jibg = x.Key,
				//				LivestockRequestItems = x.Value
				//			});

				//		if (groupedDtos != null && groupedDtos.Any())
				//		{
				//			//Dodatak Declarationa za KD
				//			if (changes.Declarations.Keys.ToList().Any(x => x == 6))
				//				changes.Declarations[6] = true;
				//			else
				//				changes.Declarations.Add(6, true);

				//			foreach (LivestockRequestItemGroupedDto item in groupedDtos)
				//			{
				//				LivestockRequestItemGroupedDto livestockRequestItemGroup = new LivestockRequestItemGroupedDto
				//				{
				//					Jibg = item.Jibg,
				//					LivestockRequestItems = item.LivestockRequestItems
				//				};

				//				List<LivestockRequestItemDto> livestockRequestItemsWithoutAnimylTypeId = livestockRequestItemGroup.LivestockRequestItems.Where(x => x.AnimalTypeId == null).ToList();

				//				if (livestockRequestItemsWithoutAnimylTypeId.Any())
				//				{
				//					List<DcanimalBreed> dcanimalBreeds = await context.DcanimalBreeds.ToListAsync();

				//					foreach (var livestockItem in livestockRequestItemsWithoutAnimylTypeId)
				//					{
				//						livestockItem.AnimalTypeId = dcanimalBreeds.FirstOrDefault(x => x.Id == livestockItem.AnimalBreedId)?.AnimalTypeId;
				//					}
				//				}

				//				IList<int> vgoAnimalBreedObligation = await context.VgoLivestockProtectedObligations.AsNoTracking()
				//					.Where(x => x.FarmId == livestockRequestItem.FarmId && x.EndYear >= livestockRequestItem.ActionContextYear && x.Active == true)
				//					.TagWith("CommonLivestockService.GetLivestockRequestGroupedItems")
				//					.Select(x => x.AnimalBreedId)
				//					.ToListAsync();

				//				if (vgoAnimalBreedObligation.Any())
				//				{
				//					List<LivestockRequestItemDto> filteredLivestockRequestItem = livestockRequestItemGroup.LivestockRequestItems
				//						.Where(x => vgoAnimalBreedObligation.Contains((int)x.AnimalBreedId))
				//						.ToList();

				//					if (filteredLivestockRequestItem.Any())
				//					{
				//						foreach (var livestockItem in filteredLivestockRequestItem)
				//						{
				//							if (livestockItem.MeasureItems.Count() < 2 && !livestockItem.MeasureItems.Any(x => x.MeasureDefinitionId == 509))
				//							{
				//								livestockItem.IsVgoChecked = null;

				//								LivestockRequestItemMeasureDto measureItem = new LivestockRequestItemMeasureDto()
				//								{
				//									LivestockId = livestockItem.LivestockId,
				//									MeasureDefinitionId = 509,
				//									MeasureId = 20,
				//									MeasureCode = "09",
				//									IsSeparatedMeasure = false,
				//									IsChecked = null,
				//									IsChangedInRequest = null
				//								};

				//								livestockItem.MeasureItems.Add(measureItem);
				//							}
				//						}
				//					}
				//				}
				//			}
				//		}

				//		changes.ChangesInternal = changes.Changes.ToDictionary((i) => i.Key, (i) => i.Value);

				//		livestockRequestItem.LivestockRequestItemsAfterUpdate = Helpers.SerializeToJsonString<LivestockRequestItemChanges>(changes);
				//	}

				await InsertLivestockRequestItemsToDb(livestockRequestItem.ActionContextId, livestockChanges, requestDocumentType);
			}
		}

		private static async Task<LivestockActionContext> CreateLivestockActionContext(ActionContextLivestockRequestItem livestockRequestItem, RequestDocumentTypeEnum requestDocumentType)
		{
			CategoryTypeEnum categoryType = requestDocumentType == RequestDocumentTypeEnum.ListC ? CategoryTypeEnum.SheetC : CategoryTypeEnum.SheetD;

			IEnumerable<LivestockDto> livestockCollection = await GetLivestock(livestockRequestItem.FarmId, (int)requestDocumentType);
			IDictionary<int, LivestockDto> livestockDictionary = livestockCollection.OrderBy(x => x.Id).ToDictionary(x => x.Id);

			IEnumerable<LivestockMeasureDto> livestockMeasures = await GetLivestockMeasures(livestockRequestItem.FarmId, (int)requestDocumentType);
			IDictionary<int, IEnumerable<LivestockMeasureDto>> livestockMeasureDictionary = livestockMeasures
				.Select(x => x.LivestockId)
				.Distinct()
				.OrderBy(x => x)
				.ToDictionary(x => x, x => livestockMeasures.Where(m => m.LivestockId == x));

			IEnumerable<LivestockCategoryDto> livestockCategories = await GetLivestockCategories(livestockRequestItem.FarmId, (int)categoryType);
			IDictionary<int, IEnumerable<LivestockCategoryDto>> livestockCategoryDictionary = livestockCategories
				.Select(x => x.LivestockId)
				.Distinct()
				.OrderBy(x => x)
				.ToDictionary(x => x, x => livestockCategories.Where(l => l.LivestockId == x));

			LivestockActionContext livestockActionContext = new LivestockActionContext()
			{
				LivestockDictionary = livestockDictionary,
				LivestockMeasureDictionary = livestockMeasureDictionary,
				LivestockCategoryDictionary = livestockCategoryDictionary
			};

			return livestockActionContext;
		}

		private static async Task<IEnumerable<LivestockDto>> GetLivestock(int farmId, int requestDocumentType)
		{
			using var context = new EditorDbContext();

			List<int> livestockIds = await context.LivestockRequestItemMeasures.AsNoTracking()
				.Where(x => x.FarmId == farmId) //TODO: Mozda dodati filter za mjere sa listaC
				.Select(x => x.LivestockId)
				.Distinct()
				.ToListAsync();

			IQueryable<Livestock> livestock = context.Livestocks.AsNoTracking()
					.Where(x => x.FarmId == farmId && livestockIds.Contains(x.Id));

			IQueryable<DcanimalBreed> animalBreeds = context.DcanimalBreeds.AsNoTracking()
				.Where(x => x.Active == true);

			IQueryable<int> livestockMeasureIds = null;

			if (requestDocumentType > 0)
				livestockMeasureIds = context.LivestockMeasures.Where(m => m.Active == true && m.FarmId == farmId && m.RequestDocumentTypeId == requestDocumentType).Select(s => s.LivestockId).Distinct();
			else
				livestockMeasureIds = context.LivestockMeasures.Where(m => m.Active == true && m.FarmId == farmId && m.RequestDocumentTypeId.HasValue).Select(s => s.LivestockId).Distinct();

			IQueryable<LivestockDto> livestockDtos =
				(from l in livestock
				 join m in livestockMeasureIds on l.Id equals m
				 join ab in animalBreeds on l.AnimalBreedId equals ab.Id into ablj
				 from subab in ablj.DefaultIfEmpty()
				 select new LivestockDto
				 {
					 Id = l.Id,
					 Active = l.Active,
					 DateCreated = l.DateCreated,
					 ProcessingId = l.ProcessingId,
					 AnimalId = l.AnimalId,
					 AnimalTypeId = subab.AnimalTypeId,
					 AnimalBreedId = l.AnimalBreedId,
					 FarmId = l.FarmId,
					 AnimalCode = l.AnimalCode,
					 Jibg = l.Jibg,
					 Ikg = l.Ikg,
					 FatherAnimalBreed = l.FatherAnimalBreed,
					 FatherAnimalCode = l.FatherAnimalCode,
					 MotherAnimalBreed = l.MotherAnimalBreed,
					 MotherAnimalCode = l.MotherAnimalCode,
					 MicrochipCode = l.MicrochipCode,
					 AnimalTypeCode = l.AnimalTypeCode,
					 AnimalBreedCode = l.AnimalBreedCode,
					 DateOfBirth = l.DateOfBirth,
					 DateOfRegistration = l.DateOfRegistration,
					 DateStartOfFattening = l.DateStartOfFattening,
					 DateDeliveryOnSlaughter = l.DateDeliveryOnSlaughter,
					 DateOfBreeding = l.DateOfBreeding,
					 DateBeginOnFarm = l.DateBeginOnFarm,
					 MilkControl = l.MilkControl,
					 SlaughterAge = l.SlaughterAge,
					 Sex = l.Sex,
					 HadOffspring = l.HadOffspring,
					 InFattening = l.InFattening,
					 IsRegistered = l.IsRegistered,
					 DateOfSlaughter = l.DateOfSlaughter,
					 IsBreedingHead = l.IsBreedingHead,
					 AnimalCategoryTypeId = l.AnimalCategoryTypeId,
					 SowRno = l.SowRno,
					 ExclusionId = l.ExclusionId,
					 Bolus = l.Bolus
				 })
				 .OrderBy(x => x.AnimalId)
				 .AsQueryable();

			return await livestockDtos.ToListAsync();
		}

		private static async Task<IEnumerable<LivestockMeasureDto>> GetLivestockMeasures(int farmId, int requestDocumentType)
		{
			using var context = new EditorDbContext();

			List<int> livestockIds = await context.LivestockRequestItemMeasures.AsNoTracking()
				.Where(x => x.FarmId == farmId) //TODO: Mozda dodati filter za mjere sa listaC
				.Select(x => x.LivestockId)
				.Distinct()
				.ToListAsync();

			IQueryable<LivestockMeasure> livestockMeasures = null;

			if (requestDocumentType > 0)
				livestockMeasures = context.LivestockMeasures.Where(m => m.Active == true && m.RequestDocumentTypeId == requestDocumentType);
			else
				livestockMeasures = context.LivestockMeasures.Where(m => m.Active == true && m.RequestDocumentTypeId.HasValue);

			IQueryable<LivestockMeasureDto> livestockMeasureDtos = context.Livestocks.AsNoTracking()
				.Where(x => x.FarmId == farmId && livestockIds.Contains(x.Id))
				.Join(livestockMeasures,
					l => l.Id,
					m => m.LivestockId,
					(l, m) => new { LivestockMeasure = m })
				.Select(s => new LivestockMeasureDto
				{
					Id = s.LivestockMeasure.Id,
					Active = s.LivestockMeasure.Active,
					DateCreated = s.LivestockMeasure.DateCreated,
					LivestockId = s.LivestockMeasure.LivestockId,
					LivestockActive = s.LivestockMeasure.LivestockActive,
					MeasureId = s.LivestockMeasure.MeasureId,
					MeasureCode = s.LivestockMeasure.MeasureCode,
					RequestDocumentTypeId = s.LivestockMeasure.RequestDocumentTypeId,
					IsSeparatedMeasure = s.LivestockMeasure.IsSeparatedMeasure,
				})
				.AsQueryable();

			return await livestockMeasureDtos.ToListAsync();
		}

		private static async Task<IEnumerable<LivestockCategoryDto>> GetLivestockCategories(int farmId, int categoryType)
		{
			using var context = new EditorDbContext();

			List<int> livestockIds = await context.LivestockRequestItemMeasures.AsNoTracking()
				.Where(x => x.FarmId == farmId) //TODO: Mozda dodati filter za mjere sa listaC
				.Select(x => x.LivestockId)
				.Distinct()
				.ToListAsync();

			IQueryable<LivestockCategory> livestockCategories = null;

			if (categoryType > 0)
				livestockCategories = context.LivestockCategories.Where(m => m.Active == true && m.CategoryTypeId == categoryType);
			else
				livestockCategories = context.LivestockCategories.Where(m => m.Active == true);

			IQueryable<LivestockCategoryDto> livestockCategoryDtos = context.Livestocks.AsNoTracking()
				.Where(x => x.FarmId == farmId && livestockIds.Contains(x.Id))
				.Join(livestockCategories,
					l => l.Id,
					c => c.LivestockId,
					(l, c) => new { LivestockCategory = c })
				.Select(s => new LivestockCategoryDto
				{
					Id = s.LivestockCategory.Id,
					Active = s.LivestockCategory.Active,
					DateCreated = s.LivestockCategory.DateCreated,
					LivestockId = s.LivestockCategory.LivestockId,
					LivestockActive = s.LivestockCategory.LivestockActive,
					CategoryId = s.LivestockCategory.CategoryId,
					CategoryTypeId = s.LivestockCategory.CategoryTypeId,
				})
				.AsQueryable();

			return await livestockCategoryDtos.ToListAsync();
		}

		private static async Task<LivestockRequestItemChanges> CreateLivestockRequestItemChanges(LivestockActionContext livestockActionContext, int year, int farmId, RequestDocumentTypeEnum requestDocumentType)
		{
			LivestockRequestItemChanges retVal = new();

			using var context = new EditorDbContext();

			IDictionary<int, LivestockDto> livestockDictionary = livestockActionContext.LivestockDictionary;
			IDictionary<int, IEnumerable<LivestockMeasureDto>> livestockMeasureDictionary = livestockActionContext.LivestockMeasureDictionary;
			IDictionary<int, IEnumerable<LivestockCategoryDto>> livestockCategoryDictionary = livestockActionContext.LivestockCategoryDictionary;

			IEnumerable<CommonMeasureDcdefinition> definitions = await context.CommonMeasureDcdefinitions.Where(x => x.Year == year && x.Active == true).ToListAsync();
			IEnumerable<Dccategory> dccategories = await context.Dccategories.ToListAsync();
			IEnumerable<DcanimalBreed> dcanimalBreeds = await context.DcanimalBreeds.ToListAsync();
			IEnumerable<DcanimalType> dcanimalTypes = await context.DcanimalTypes.ToListAsync();

			IEnumerable<VgoLivestockProtectedObligation> vgoLivestockObligation = await context.VgoLivestockProtectedObligations.AsNoTracking()
			.Where(x => x.FarmId == farmId && x.EndYear >= year && x.Active == true)
			.TagWith("CommonLivestockService.CreateLivestockRequestItemChanges")
			.ToListAsync();

			List<int> declarationIds = livestockMeasureDictionary.Values
				.SelectMany(x => x)
				.GroupBy(x => x.MeasureId)
				.Join(definitions,
					m => m.Key,
					d => d.MeasureId,
					(m, d) => new { DeclarationId = d.DeclarationId })
				.GroupBy(x => x.DeclarationId)
				.Select(d => d.Key.GetValueOrDefault()).ToList();

			declarationIds.ForEach(x => retVal.Declarations.Add(x, false));

			if (requestDocumentType == RequestDocumentTypeEnum.ListC)
			{
				foreach (int enabledDeclaration in AlwaysEnabledDeclaration)
				{
					if (retVal.Declarations.ContainsKey(enabledDeclaration))
						retVal.Declarations[enabledDeclaration] = true;
					else
						retVal.Declarations.Add(enabledDeclaration, true);
				}
			}

			foreach (var kvp in livestockDictionary.OrderBy(x => x.Value.Id))
			{
				LivestockDto livestock = kvp.Value;
				livestockMeasureDictionary.TryGetValue(livestock.Id, out IEnumerable<LivestockMeasureDto> livestockMeasures);
				livestockCategoryDictionary.TryGetValue(livestock.Id, out IEnumerable<LivestockCategoryDto> livestockCategories);

				IEnumerable<LivestockRequestItemMeasureDto>? measures = livestockMeasures?.Select(x => new LivestockRequestItemMeasureDto
				{
					LivestockId = x.LivestockId,
					MeasureDefinitionId = null,
					MeasureId = x.MeasureId,
					MeasureCode = x.MeasureCode,
					IsSeparatedMeasure = x.IsSeparatedMeasure,
					IsChecked = null,
					IsChangedInRequest = null,
				})
				.ToList();

				IEnumerable<LivestockRequestItemCategoryDto>? categories = livestockCategories?.Select(x => new LivestockRequestItemCategoryDto
				{
					LivestockId = x.LivestockId,
					CategoryId = x.CategoryId
				})
				.ToList();

				if (measures != null)
				{
					if (requestDocumentType == RequestDocumentTypeEnum.ListC)
					{
						measures = (
							from m in measures
							join d in definitions on m.MeasureId equals d.MeasureId
							select new { Item = m, MeasureDefinitionId = d.Id })
							.Select(x =>
							{
								x.Item.MeasureDefinitionId = x.MeasureDefinitionId;
								x.Item.LivestockId = livestock.Id;
								x.Item.IsChecked = true;
								return x.Item;
							});
					}
					else if (requestDocumentType == RequestDocumentTypeEnum.ListD)
					{
						bool isFarmInVgoObligation = vgoLivestockObligation.Where(x => x.AnimalBreedId == livestock.AnimalBreedId).Any();

						measures = (
							from m in measures
							join d in definitions on m.MeasureId equals d.MeasureId
							where isFarmInVgoObligation == true || d.MeasurePillarId == (int)CommonMeasurePillarEnum.RuralInterventions
							select new LivestockRequestItemMeasureDto
							{
								LivestockId = m.LivestockId,
								MeasureDefinitionId = d.Id,
								MeasureId = m.MeasureId,
								MeasureCode = m.MeasureCode,
								IsSeparatedMeasure = m.IsSeparatedMeasure,
								IsChecked = m.IsChecked,
								IsChangedInRequest = m.IsChangedInRequest
							})
							.Select(x =>
							{
								x.MeasureDefinitionId = x.MeasureDefinitionId;
								x.LivestockId = livestock.Id;
								x.IsChecked = isFarmInVgoObligation == true ? null : true;
								return x;
							});
					}
				}

				LivestockRequestItemDto item = retVal.CreateLivestockRequestItem(livestock, measures, categories, dccategories, dcanimalBreeds, dcanimalTypes);

				retVal.Add(item);
			}

			return retVal;
		}

		private static async Task InsertLivestockRequestItemsToDb(Guid actionContextId, LivestockRequestItemChanges changes, RequestDocumentTypeEnum requestDocumentType = RequestDocumentTypeEnum.ListC)
		{
			try
			{
				using var context = new EditorDbContext();

				List<SqlParameter> cmdParameters = new List<SqlParameter>();
				cmdParameters.Add(new SqlParameter("@ActionContextId", actionContextId));
				cmdParameters.Add(new SqlParameter("@RequestDocumentTypeId", (int)requestDocumentType));
				SqlParameter jsonParameter = new("@RequestItems", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Input, Value = Helpers.SerializeToJsonString<LivestockRequestItemChanges>(changes) };
				cmdParameters.Add(jsonParameter);

				string sqlStatement = @"EXEC [Payments].[ActionContext.Livestock.RequestItems.Create] @ActionContextId, @RequestDocumentTypeId, @CreatedBy, @RequestItems ";
				await context.Database.ExecuteSqlRawAsync(sqlStatement, cmdParameters.ToArray());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}

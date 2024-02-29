using CropMeasureEligibility.Editor.Models.ActionContext;
using System.Text.Json.Serialization;

namespace CropMeasureEligibility.Editor.Models.ListD
{
	internal class LivestockRequestItemChanges
	{
		public Dictionary<int, LivestockRequestItemDto> Changes { get; set; } = new Dictionary<int, LivestockRequestItemDto>();

		public Dictionary<int, bool> Declarations { get; set; } = new Dictionary<int, bool>();

		// serialization will trigger property updates

		public LivestockRequestItemsWarningDto WarningDto { get; set; } = new();

		public int ChangesCount { get; set; }

		public IDictionary<int, LivestockRequestItemDto> ChangesInternal { get; set; }

		private bool HasBeenAltered
		{
			set
			{
				if (value)
				{
					//IsRefreshOfChangesRequired = value;
				}
			}
		}

		//[JsonIgnore]
		//public bool IsRefreshOfChangesRequired
		//{
		//	get { return _isRefreshOfChangesSheetCRequired || ChangesInternal == null; }
		//	set { _isRefreshOfChangesSheetCRequired = value; }
		//}

		//[JsonIgnore]
		//public IDictionary<int, LivestockRequestItemDto> LivestockChanges
		//{
		//	get
		//	{
		//		if (IsRefreshOfChangesRequired)
		//		{
		//			ChangesInternal = new Dictionary<int, LivestockRequestItemDto>();

		//			foreach (KeyValuePair<int, LivestockRequestItemDto> keyValuePair in Changes)
		//			{
		//				LivestockRequestItemDto livestockItem = keyValuePair.Value;
		//				ChangesInternal.Add(livestockItem.LivestockId, livestockItem);
		//			}

		//			IsRefreshOfChangesRequired = false;
		//		}

		//		return ChangesInternal;
		//	}
		//}

		#region Methods

		public void Add(LivestockRequestItemDto item)
		{
			int livestockId = item.LivestockId;
			bool contains = Changes.ContainsKey(livestockId);

			if (contains)
				Changes[livestockId] = item;
			else
				Changes.Add(livestockId, item);

			HasBeenAltered = true;
		}

		public void Add(int key, LivestockRequestItemDto value)
		{
			Changes.Add(key, value);
			HasBeenAltered = true;
		}

		public void Remove(int key)
		{
			Changes.Remove(key);
			HasBeenAltered = true;
		}

		public void AddLivestockMeasure(LivestockRequestItemMeasureDto item)
		{
			bool contains = Changes.ContainsKey(item.LivestockId);
			if (contains)
			{
				LivestockRequestItemDto livestockItem = Changes[item.LivestockId];
				List<LivestockRequestItemMeasureDto> measureItems = livestockItem.MeasureItems;
				if (measureItems == null)
					measureItems = new List<LivestockRequestItemMeasureDto>();

				LivestockRequestItemMeasureDto measureDto = measureItems.FirstOrDefault(x => x.MeasureDefinitionId == item.MeasureDefinitionId);
				if (measureDto != null)
					measureItems.Remove(measureDto);

				measureItems.Add(item);

				livestockItem.MeasureItems = measureItems;
				Add(livestockItem);
			}
		}

		public void RemoveLivestockMeasure(LivestockRequestItemMeasureDto item)
		{
			bool contains = Changes.ContainsKey(item.LivestockId);
			if (contains)
			{
				LivestockRequestItemDto livestockItem = Changes[item.LivestockId];
				List<LivestockRequestItemMeasureDto> measureItems = livestockItem.MeasureItems;
				if (measureItems == null)
					measureItems = new List<LivestockRequestItemMeasureDto>();

				LivestockRequestItemMeasureDto measureDto = measureItems.FirstOrDefault(x => x.MeasureDefinitionId == item.MeasureDefinitionId);
				if (measureDto != null)
					measureItems.Remove(measureDto);

				livestockItem.MeasureItems = measureItems;
				Add(livestockItem);
			}
		}

		public void AddLivestockCategory(LivestockRequestItemCategoryDto item)
		{
			bool contains = Changes.ContainsKey(item.LivestockId);
			if (contains)
			{
				LivestockRequestItemDto livestockItem = Changes[item.LivestockId];
				List<LivestockRequestItemCategoryDto> categoryItems = livestockItem.CategoryItems;
				if (categoryItems == null)
					categoryItems = new List<LivestockRequestItemCategoryDto>();

				LivestockRequestItemCategoryDto categoryDto = categoryItems.FirstOrDefault(x => x.CategoryId == item.CategoryId);
				if (categoryDto != null)
					categoryItems.Remove(categoryDto);

				categoryItems.Add(item);

				livestockItem.CategoryItems = categoryItems;
				Add(livestockItem);
			}
		}

		public void RemoveLivestockCategory(LivestockRequestItemCategoryDto item)
		{
			bool contains = Changes.ContainsKey(item.LivestockId);
			if (contains)
			{
				LivestockRequestItemDto livestockItem = Changes[item.LivestockId];
				List<LivestockRequestItemCategoryDto> categoryItems = livestockItem.CategoryItems;
				if (categoryItems == null)
					categoryItems = new List<LivestockRequestItemCategoryDto>();

				LivestockRequestItemCategoryDto categoryDto = categoryItems.FirstOrDefault(x => x.CategoryId == item.CategoryId);
				if (categoryDto != null)
					categoryItems.Remove(categoryDto);

				livestockItem.CategoryItems = categoryItems;
				Add(livestockItem);
			}
		}

		public LivestockRequestItemDto CreateLivestockRequestItem(
			LivestockDto livestock,
			IEnumerable<LivestockRequestItemMeasureDto> measureItems,
			IEnumerable<LivestockRequestItemCategoryDto> categoryItems,
			IEnumerable<Dccategory> dccategories,
			IEnumerable<DcanimalBreed> dcanimalBreeds,
			IEnumerable<DcanimalType> dcanimalTypes)
		{
			LivestockRequestItemDto retVal = CreateLivestockRequestItem(livestock);
			retVal.MeasureItems = measureItems.ToList();
			retVal.CategoryItems = categoryItems.ToList();

			DcanimalBreed dcanimalBreed = dcanimalBreeds.FirstOrDefault(x => x.AnimalTypeCode == retVal.AnimalTypeCode && x.Code == retVal.AnimalBreedCode);
			retVal.AnimalBreedName = dcanimalBreed?.Name ?? string.Empty;

			int categoryId = categoryItems.First().CategoryId;
			Dccategory dccategory = dccategories.FirstOrDefault(x => x.Id == categoryId);
			retVal.CategoryId = categoryId;
			retVal.CategoryName = dccategory?.LegendName ?? string.Empty;

			DcanimalType dcanimalType = dcanimalTypes.FirstOrDefault(x => x.Code == retVal.AnimalTypeCode);
			retVal.AnimalTypeName = dcanimalType?.Name;

			return retVal;
		}

		private static LivestockRequestItemDto CreateLivestockRequestItem(LivestockDto livestock)
		{
			LivestockRequestItemDto retVal = new LivestockRequestItemDto()
			{
				Identifier = Guid.NewGuid(),
				LivestockId = livestock.Id,
				AnimalTypeId = livestock.AnimalTypeId,
				AnimalBreedId = livestock.AnimalBreedId,
				AnimalCode = livestock.AnimalCode,
				Jibg = livestock.Jibg,
				Ikg = livestock.Ikg,
				MicrochipCode = livestock.MicrochipCode,
				AnimalTypeCode = livestock.AnimalTypeCode,
				AnimalBreedCode = livestock.AnimalBreedCode,
				DateOfBirth = livestock.DateOfBirth,
				AnimalCategoryTypeId = livestock.AnimalCategoryTypeId,
				Sex = livestock.Sex
			};

			return retVal;
		}

		#endregion
	}
}

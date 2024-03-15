namespace CropMeasureEligibility.Editor.Models.ListD
{
	internal class LivestockRequestItemDto
	{
		public Guid Identifier { get; set; }
		public int LivestockId { get; set; }
		public int? AnimalTypeId { get; set; }
		public int? AnimalBreedId { get; set; }
		public string AnimalCode { get; set; }
		public string Jibg { get; set; }
		public string Ikg { get; set; }
		public string MicrochipCode { get; set; }
		public string AnimalTypeCode { get; set; }
		public string AnimalBreedCode { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public int? AnimalCategoryTypeId { get; set; }
		public string Sex { get; set; }
		public int? CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string AnimalBreedName { get; set; }
		public string AnimalTypeName { get; set; }

		public bool? NotSelectedDoubleMeasures { get; set; }
		public bool? WrongCombination { get; set; }
		public decimal? ConditionalNeckCoefficient { get; set; }
		public bool? IsVgoChecked { get; set; }


		public List<LivestockRequestItemMeasureDto>? MeasureItems { get; set; }
		public List<LivestockRequestItemCategoryDto>? CategoryItems { get; set; }
	}
}

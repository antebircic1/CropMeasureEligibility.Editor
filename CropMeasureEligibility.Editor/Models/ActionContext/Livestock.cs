namespace CropMeasureEligibility.Editor.Models.ActionContext
{
	internal class Livestock
	{
		public int Id { get; set; }
		public bool? Active { get; set; }
		public DateTime DateCreated { get; set; }
		public int ProcessingId { get; set; }
		public DateTime? DateModified { get; set; }
		public int? ModifiedProcessingId { get; set; }
		public int AnimalId { get; set; }
		public int? AnimalBreedId { get; set; }
		public int? FarmId { get; set; }
		public string AnimalCode { get; set; }
		public string? Jibg { get; set; }
		public string? Ikg { get; set; }
		public string? FatherAnimalBreed { get; set; }
		public string? FatherAnimalCode { get; set; }
		public string? MotherAnimalBreed { get; set; }
		public string? MotherAnimalCode { get; set; }
		public string? MicrochipCode { get; set; }
		public string? AnimalTypeCode { get; set; }
		public string? AnimalBreedCode { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public DateTime? DateOfRegistration { get; set; }
		public DateTime? DateStartOfFattening { get; set; }
		public DateTime? DateDeliveryOnSlaughter { get; set; }
		public DateTime? DateOfBreeding { get; set; }
		public DateTime? DateBeginOnFarm { get; set; }
		public bool? MilkControl { get; set; }
		public int? SlaughterAge { get; set; }
		public string Sex { get; set; }
		public bool? HadOffspring { get; set; }
		public bool? InFattening { get; set; }
		public bool? IsRegistered { get; set; }
		public DateTime? DateOfSlaughter { get; set; }
		public bool? IsBreedingHead { get; set; }
		public int? AnimalCategoryTypeId { get; set; }
		public int? SowRno { get; set; }
		public int? ExclusionId { get; set; }
		public bool? Bolus { get; set; }
		public byte[] RowVersion { get; set; }
	}
}

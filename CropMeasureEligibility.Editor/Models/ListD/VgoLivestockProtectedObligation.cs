namespace CropMeasureEligibility.Editor.Models.ListD
{
	internal class VgoLivestockProtectedObligation
	{
		public int Id { get; set; }
		public bool? Active { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateDeactivated { get; set; }
		public int FarmId { get; set; }
		public int FirstYear { get; set; }
		public int EndYear { get; set; }
		public int YearsInCommitment { get; set; }
		public int AnimalBreedId { get; set; }
		public int AnimalTypeId { get; set; }
		public decimal Quantity { get; set; }
		public decimal UgQuantity { get; set; }
	}
}

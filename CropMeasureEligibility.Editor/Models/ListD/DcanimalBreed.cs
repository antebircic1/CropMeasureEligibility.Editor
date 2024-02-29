namespace CropMeasureEligibility.Editor.Models.ListD
{
	internal class DcanimalBreed
	{
		public int Id { get; set; }
		public bool? Active { get; set; }
		public DateTime DateCreated { get; set; }
		public int AnimalTypeId { get; set; }
		public string AnimalTypeCode { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public bool? IsProtected { get; set; }
	}
}

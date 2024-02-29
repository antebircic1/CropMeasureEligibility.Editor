namespace CropMeasureEligibility.Editor.Models.ActionContext
{
	public class LivestockCategoryDto
	{
		public int Id { get; set; }
		public bool? Active { get; set; }
		public DateTime DateCreated { get; set; }
		public int LivestockId { get; set; }
		public bool LivestockActive { get; set; }
		public int CategoryId { get; set; }
		public int CategoryTypeId { get; set; }
	}
}

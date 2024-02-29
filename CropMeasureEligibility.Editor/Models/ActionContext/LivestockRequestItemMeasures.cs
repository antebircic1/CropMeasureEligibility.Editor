namespace CropMeasureEligibility.Editor.Models.ActionContext
{
	internal class LivestockRequestItemMeasures
	{
		public int Id { get; set; }
		public int FarmId { get; set; }
		public int SubmissionId { get; set; }
		public int BarcodeId { get; set; }
		public bool? Active { get; set; }
		public DateTime DateCreated { get; set; }
		public int RequestDocumentId { get; set; }
		public int LivestockId { get; set; }
		public int CategoryId { get; set; }
		public int RequestItemId { get; set; }
		public int MeasureDefinitionId { get; set; }
		public bool Checked { get; set; }
	}
}

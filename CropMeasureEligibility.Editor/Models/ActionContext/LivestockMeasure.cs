namespace CropMeasureEligibility.Editor.Models.ActionContext
{
	internal class LivestockMeasure
	{
		public long Id { get; set; }
		public bool? Active { get; set; }
		public DateTime DateCreated { get; set; }
		public int ProcessingId { get; set; }
		public int LivestockId { get; set; }
		public bool LivestockActive { get; set; }
		public int MeasureId { get; set; }
		public string MeasureCode { get; set; }
		public int? RequestDocumentTypeId { get; set; }
		public bool? IsSeparatedMeasure { get; set; }
		public int? FarmId { get; set; }
	}
}

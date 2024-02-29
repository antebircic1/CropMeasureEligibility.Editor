namespace CropMeasureEligibility.Editor.Models.ListD
{
	internal class LivestockRequestItemMeasureDto
	{
		public int LivestockId { get; set; }
		public int? MeasureDefinitionId { get; set; }
		public int MeasureId { get; set; }
		public string MeasureCode { get; set; }
		public bool? IsSeparatedMeasure { get; set; }
		public bool? IsChecked { get; set; }
		public bool? IsChangedInRequest { get; set; }
	}
}

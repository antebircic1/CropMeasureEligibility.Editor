namespace CropMeasureEligibility.Editor.Models.ListD
{
	internal class LivestockRequestItemGroupedDto
	{
		public string Jibg { get; set; }
		public IEnumerable<LivestockRequestSummaryItemDto> SummaryItems { get; set; }
		public IEnumerable<LivestockRequestItemDto> LivestockRequestItems { get; set; }
	}
}

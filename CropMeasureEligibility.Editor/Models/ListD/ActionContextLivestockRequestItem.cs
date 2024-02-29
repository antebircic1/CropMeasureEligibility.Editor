namespace CropMeasureEligibility.Editor.Models.ListD
{
	internal class ActionContextLivestockRequestItem
	{
		public int Id { get; set; }
		public Guid ActionContextId { get; set; }
		public int FarmId { get; set; }
		public int ActionContextYear { get; set; }
		public int RequestDocumentTypeId { get; set; }
		public string LivestockRequestItems { get; set; }
		public string LivestockRequestItemsAfterUpdate { get; set; }
		public string LivestockDictionaryAfterUpdate { get; set; }
	}
}

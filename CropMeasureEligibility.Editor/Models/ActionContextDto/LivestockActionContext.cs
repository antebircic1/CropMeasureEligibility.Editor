namespace CropMeasureEligibility.Editor.Models.ActionContext
{
	public class LivestockActionContext
	{
		public IDictionary<int, LivestockDto> LivestockDictionary { get; set; } = new Dictionary<int, LivestockDto>();
		public IDictionary<int, IEnumerable<LivestockMeasureDto>> LivestockMeasureDictionary { get; set; } = new Dictionary<int, IEnumerable<LivestockMeasureDto>>();
		public IDictionary<int, IEnumerable<LivestockCategoryDto>> LivestockCategoryDictionary { get; set; } = new Dictionary<int, IEnumerable<LivestockCategoryDto>>();
	}
}

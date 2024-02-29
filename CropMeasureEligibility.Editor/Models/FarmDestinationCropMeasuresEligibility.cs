namespace CropMeasureEligibility.Editor.Models
{
    internal class FarmDestinationCropMeasuresEligibility
    {
        public int Id { get; set; }
        public int FarmId { get; set; }
        public Guid ActionContextId { get; set; }
        public string CropMeasureEligibility { get; set; }
    }
}

namespace CropMeasureEligibility.Editor.Models
{
    public class ArkodCropMeasureEligibilityDto
    {
        public Guid Identifier { get; set; }
        public int MeasureDefinitionId { get; set; }
        public int CropMeasureGroupId { get; set; }
        public int CropGroupId { get; set; }
        public bool IsVgo { get; set; }
        public int? ObligationYearEnd { get; set; }
    }
}

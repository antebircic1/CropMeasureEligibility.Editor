namespace CropMeasureEligibility.Editor.Models.ActionContext
{
	internal class CommonMeasureDcdefinition
	{
		public int Id { get; set; }
		public bool? Active { get; set; }
		public int Year { get; set; }
		public int MeasureId { get; set; }
		public int? MeasurePillarId { get; set; }
		public int? MeasureGroupId { get; set; }
		public int? DeclarationId { get; set; }
		public int? ControlTypeId { get; set; }
		public int? InitialPhaseId { get; set; }
		public int? RequestDocumentTypeId { get; set; }
		public string MeasureName { get; set; }
		public bool? IsVisbileOnSummary { get; set; }
		public string? Code { get; set; }
		public int ProductionTypeId { get; set; }
		public bool IsDelayRedCalcUsingDecId { get; set; }
		public int? NoticeTypeId { get; set; }
		public int CurrencyId { get; set; }
		public string PaymentMeasureName { get; set; }
		public string? FocusArea { get; set; }
		public bool? IsObligationReductionApplicable { get; set; }
		public string NoticeName { get; set; }
		public bool? IsForDecisionAdministration { get; set; }
		public int QuantitiesFundSourceId { get; set; }
		public bool? IsForPublish { get; set; }
		public int? OperationTypeId { get; set; }
		public bool? IsVgoapplicable { get; set; }
		public string? MeasureCode { get; set; }
	}
}

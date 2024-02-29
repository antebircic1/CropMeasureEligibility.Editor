using CropMeasureEligibility.Editor.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CropMeasureEligibility.Editor
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			#region Generate new PDFs
			//using var context = new EditorDbContext();

			//var farmIdBarcodeIds = await context.FarmIdBarcodeIds.ToListAsync();

			//foreach(var farmIdBarcodeId in farmIdBarcodeIds)
			//{
			//    await Service.Service.GeneratePdfFile(submissionId: farmIdBarcodeId.Barcode, farmId: farmIdBarcodeId.FarmId);
			//}
			#endregion

			#region In Memory JSON manipulation

			//await Service.Service.UpdateJsonToFile();

			#endregion

			#region Database JSON manipulation

			//await Service.Service.UpdateJsonToDb();

			#endregion

			#region Database JSON ListD

			await Service.Service.UpdateJsonListD();

			#endregion

			#region Database JSON ListC

			//await Service.Service.UpdateJsonListC();

			#endregion

			#region Database Create ActionContext

			//await Service.Service.CreateActionContext(RequestDocumentTypeEnum.ListC);

			#endregion
		}
	}
}
using CropMeasureEligibility.Editor.Models;
using CropMeasureEligibility.Editor.Models.ActionContext;
using CropMeasureEligibility.Editor.Models.ListD;
using Microsoft.EntityFrameworkCore;

namespace CropMeasureEligibility.Editor.Infrastructure.Persistance
{
	internal class EditorDbContext : DbContext
	{
		public DbSet<FarmSourceCropMeasuresEligibility> FarmSourceCropMeasuresEligibilityes { get; set; }
		public DbSet<FarmDestinationCropMeasuresEligibility> FarmDestinationCropMeasuresEligibilityes { get; set; }
		public DbSet<ActionContextIdIdentifier> ActionContextIdIdentifiers { get; set; }
		public DbSet<FarmIdBarcodeId> FarmIdBarcodeIds { get; set; }

		#region ListD

		public DbSet<ActionContextLivestockRequestItem> ActionContextLivestockRequestItems { get; set; }
		public DbSet<VgoLivestockProtectedObligation> VgoLivestockProtectedObligations { get; set; }
		public DbSet<DcanimalBreed> DcanimalBreeds { get; set; }

		#endregion

		#region ActionContext

		public DbSet<Livestock> Livestocks { get; set; }
		public DbSet<LivestockMeasure> LivestockMeasures { get; set; }
		public DbSet<LivestockCategory> LivestockCategories { get; set; }
		public DbSet<LivestockRequestItemMeasures> LivestockRequestItemMeasures { get; set; }
		public DbSet<CommonMeasureDcdefinition> CommonMeasureDcdefinitions { get; set; }
		public DbSet<Dccategory> Dccategories { get; set; }
		public DbSet<DcanimalType> DcanimalTypes { get; set; }

		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FarmSourceCropMeasuresEligibility>(x =>
				x.HasKey(x => x.Id)
			);

			modelBuilder.Entity<FarmDestinationCropMeasuresEligibility>(x =>
				x.HasKey(x => x.Id)
			);

			modelBuilder.Entity<ActionContextIdIdentifier>(x =>
				x.HasKey(x => x.Id)
			);

			modelBuilder.Entity<FarmIdBarcodeId>(x =>
				x.HasKey(x => x.Id)
			);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=.\;Database=JSONUpdater;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
		}
	}
}

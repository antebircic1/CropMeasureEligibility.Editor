using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropMeasureEligibility.Editor.Migrations
{
    /// <inheritdoc />
    public partial class AddedActionContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LivestockDictionaryAfterUpdate",
                table: "ActionContextLivestockRequestItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CommonMeasureDcdefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    MeasureId = table.Column<int>(type: "int", nullable: false),
                    MeasurePillarId = table.Column<int>(type: "int", nullable: true),
                    MeasureGroupId = table.Column<int>(type: "int", nullable: true),
                    DeclarationId = table.Column<int>(type: "int", nullable: true),
                    ControlTypeId = table.Column<int>(type: "int", nullable: true),
                    InitialPhaseId = table.Column<int>(type: "int", nullable: true),
                    RequestDocumentTypeId = table.Column<int>(type: "int", nullable: true),
                    MeasureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVisbileOnSummary = table.Column<bool>(type: "bit", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDelayRedCalcUsingDecId = table.Column<bool>(type: "bit", nullable: false),
                    NoticeTypeId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    PaymentMeasureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FocusArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsObligationReductionApplicable = table.Column<bool>(type: "bit", nullable: true),
                    NoticeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsForDecisionAdministration = table.Column<bool>(type: "bit", nullable: true),
                    QuantitiesFundSourceId = table.Column<int>(type: "int", nullable: false),
                    IsForPublish = table.Column<bool>(type: "bit", nullable: true),
                    OperationTypeId = table.Column<int>(type: "int", nullable: true),
                    IsVgoapplicable = table.Column<bool>(type: "bit", nullable: true),
                    MeasureCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonMeasureDcdefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DcanimalTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DcanimalTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dccategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegendName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dccategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LivestockCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessingId = table.Column<int>(type: "int", nullable: false),
                    LivestockId = table.Column<int>(type: "int", nullable: false),
                    LivestockActive = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryTypeId = table.Column<int>(type: "int", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivestockCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LivestockMeasures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessingId = table.Column<int>(type: "int", nullable: false),
                    LivestockId = table.Column<int>(type: "int", nullable: false),
                    LivestockActive = table.Column<bool>(type: "bit", nullable: false),
                    MeasureId = table.Column<int>(type: "int", nullable: false),
                    MeasureCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDocumentTypeId = table.Column<int>(type: "int", nullable: true),
                    IsSeparatedMeasure = table.Column<bool>(type: "bit", nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivestockMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LivestockRequestItemMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    SubmissionId = table.Column<int>(type: "int", nullable: false),
                    BarcodeId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestDocumentId = table.Column<int>(type: "int", nullable: false),
                    LivestockId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    RequestItemId = table.Column<int>(type: "int", nullable: false),
                    MeasureDefinitionId = table.Column<int>(type: "int", nullable: false),
                    Checked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivestockRequestItemMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Livestocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessingId = table.Column<int>(type: "int", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedProcessingId = table.Column<int>(type: "int", nullable: true),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    AnimalBreedId = table.Column<int>(type: "int", nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: true),
                    AnimalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jibg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ikg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherAnimalBreed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherAnimalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherAnimalBreed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherAnimalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MicrochipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnimalBreedCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfRegistration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateStartOfFattening = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeliveryOnSlaughter = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfBreeding = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateBeginOnFarm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MilkControl = table.Column<bool>(type: "bit", nullable: true),
                    SlaughterAge = table.Column<int>(type: "int", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HadOffspring = table.Column<bool>(type: "bit", nullable: true),
                    InFattening = table.Column<bool>(type: "bit", nullable: true),
                    IsRegistered = table.Column<bool>(type: "bit", nullable: true),
                    DateOfSlaughter = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsBreedingHead = table.Column<bool>(type: "bit", nullable: true),
                    AnimalCategoryTypeId = table.Column<int>(type: "int", nullable: true),
                    SowRno = table.Column<int>(type: "int", nullable: true),
                    ExclusionId = table.Column<int>(type: "int", nullable: true),
                    Bolus = table.Column<bool>(type: "bit", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livestocks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommonMeasureDcdefinitions");

            migrationBuilder.DropTable(
                name: "DcanimalTypes");

            migrationBuilder.DropTable(
                name: "Dccategories");

            migrationBuilder.DropTable(
                name: "LivestockCategories");

            migrationBuilder.DropTable(
                name: "LivestockMeasures");

            migrationBuilder.DropTable(
                name: "LivestockRequestItemMeasures");

            migrationBuilder.DropTable(
                name: "Livestocks");

            migrationBuilder.DropColumn(
                name: "LivestockDictionaryAfterUpdate",
                table: "ActionContextLivestockRequestItems");
        }
    }
}

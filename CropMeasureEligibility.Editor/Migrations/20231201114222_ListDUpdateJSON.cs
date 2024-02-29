using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropMeasureEligibility.Editor.Migrations
{
    /// <inheritdoc />
    public partial class ListDUpdateJSON : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionContextLivestockRequestItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionContextId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    ActionContextYear = table.Column<int>(type: "int", nullable: false),
                    RequestDocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    LivestockRequestItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LivestockRequestItemsAfterUpdate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionContextLivestockRequestItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DcanimalBreeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnimalTypeId = table.Column<int>(type: "int", nullable: false),
                    AnimalTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsProtected = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DcanimalBreeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VgoLivestockProtectedObligations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeactivated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    FirstYear = table.Column<int>(type: "int", nullable: false),
                    EndYear = table.Column<int>(type: "int", nullable: false),
                    YearsInCommitment = table.Column<int>(type: "int", nullable: false),
                    AnimalBreedId = table.Column<int>(type: "int", nullable: false),
                    AnimalTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UgQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VgoLivestockProtectedObligations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionContextLivestockRequestItems");

            migrationBuilder.DropTable(
                name: "DcanimalBreeds");

            migrationBuilder.DropTable(
                name: "VgoLivestockProtectedObligations");
        }
    }
}

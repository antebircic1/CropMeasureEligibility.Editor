using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropMeasureEligibility.Editor.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FarmDestinationCropMeasuresEligibilityes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    ActionContextId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CropMeasureEligibility = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmDestinationCropMeasuresEligibilityes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmSourceCropMeasuresEligibilityes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmId = table.Column<int>(type: "int", nullable: false),
                    ActionContextId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CropMeasureEligibility = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmSourceCropMeasuresEligibilityes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmDestinationCropMeasuresEligibilityes");

            migrationBuilder.DropTable(
                name: "FarmSourceCropMeasuresEligibilityes");
        }
    }
}

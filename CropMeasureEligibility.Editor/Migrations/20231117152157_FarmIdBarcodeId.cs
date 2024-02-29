using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropMeasureEligibility.Editor.Migrations
{
    /// <inheritdoc />
    public partial class FarmIdBarcodeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FarmIdBarcodeIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmIdBarcodeIds", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FarmIdBarcodeIds");
        }
    }
}

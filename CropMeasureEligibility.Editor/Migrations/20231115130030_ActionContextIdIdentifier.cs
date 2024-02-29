using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropMeasureEligibility.Editor.Migrations
{
    /// <inheritdoc />
    public partial class ActionContextIdIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionContextIdIdentifiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionContextId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionContextIdIdentifiers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionContextIdIdentifiers");
        }
    }
}

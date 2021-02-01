using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetsOnMarket.Infrastructure.Data.Migrations
{
    public partial class AddInitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Asset",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(nullable: false),
                    AssetName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetProperty",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(nullable: false),
                    Property = table.Column<string>(maxLength: 255, nullable: false),
                    Value = table.Column<bool>(nullable: false, defaultValue: false),
                    Timestamp = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_AssetProperty",
                        column: x => x.AssetId,
                        principalSchema: "dbo",
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UX_AssetId_Property",
                schema: "dbo",
                table: "AssetProperty",
                columns: new[] { "AssetId", "Property" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetProperty",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Asset",
                schema: "dbo");
        }
    }
}

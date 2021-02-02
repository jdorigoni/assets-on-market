using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace AssetsOnMarket.Infrastructure.Data.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(
                Path.Combine(Directory.GetCurrentDirectory(),
                "../AssetsOnMarket.Infrastructure.Data/Migrations/SeedScripts/InsertInitialSeedData.sql")));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(File.ReadAllText(
                Path.Combine(Directory.GetCurrentDirectory(),
                    "../AssetsOnMarket.Infrastructure.Data/Migrations/SeedScripts/DeleteInitialSeedData.sql")));
        }
    }
}

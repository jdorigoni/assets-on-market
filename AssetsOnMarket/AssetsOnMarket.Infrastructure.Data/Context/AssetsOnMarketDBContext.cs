using AssetsOnMarket.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AssetsOnMarket.Infrastructure.Data.Context
{
    public class AssetsOnMarketDBContext : DbContext
    {
        public AssetsOnMarketDBContext(DbContextOptions<AssetsOnMarketDBContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AssetConfiguration());
            modelBuilder.ApplyConfiguration(new AssetPropertyConfiguration());
        }
    }
}

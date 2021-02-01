using AssetsOnMarket.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetsOnMarket.Infrastructure.Data.Configuration
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("Asset", "dbo");

            builder.Property(a => a.AssetId)
                .IsRequired();

            builder.Property(a => a.AssetName)
                .HasMaxLength(255);
        }
    }
}

using AssetsOnMarket.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AssetsOnMarket.Infrastructure.Data.Configuration
{
    public class AssetPropertyConfiguration : IEntityTypeConfiguration<AssetProperty>
    {
        public void Configure(EntityTypeBuilder<AssetProperty> builder)
        {
            builder.ToTable("AssetProperty", "dbo");

            builder.Property(ap => ap.Property)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ap => ap.Value)
                .HasDefaultValue(false);

            builder.Property(ap => ap.Timestamp)
                .HasDefaultValue(DateTime.MinValue);

            builder.HasOne(ap => ap.Asset)
                .WithMany(a => a.AssetProperties)
                .HasForeignKey(ap => ap.AssetId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Asset_AssetProperty");

            builder.HasIndex(ap => new { ap.AssetId, ap.Property})
                .IsUnique()
                .HasName("UX_AssetId_Property");
        }
    }
}

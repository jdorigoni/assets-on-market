using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AssetsOnMarket.Domain.Interfaces;
using AssetsOnMarket.Domain.Models;
using AssetsOnMarket.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AssetsOnMarket.Infrastructure.Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetsOnMarketDBContext _context;
        private readonly DbSet<AssetProperty> _dbSetAssetProperty;
        private readonly DbSet<Asset> _dbSetAsset;

        public AssetRepository(AssetsOnMarketDBContext context)
        {
            _context = context ?? throw new ArgumentNullException($"{context}");
            _dbSetAssetProperty = _context.Set<AssetProperty>();
            _dbSetAsset = _context.Set<Asset>();
        }

        public async Task<IEnumerable<AssetProperty>> GetAssetProperty(Expression<Func<AssetProperty, bool>> filter = null)
        {
            IQueryable<AssetProperty> queryable = _dbSetAssetProperty;
            return await queryable.Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssets(Expression<Func<Asset, bool>> filter = null)
        {
            IQueryable<Asset> queryable = _dbSetAsset;
            return await queryable.Where(filter).ToListAsync();
        }

        public async Task InsertAsset(Asset asset)
        {
            if (asset == null) throw new ArgumentException($"asset");

            await _dbSetAsset.AddAsync(asset);
        }

        public async Task InsertAssetProperty(AssetProperty assetProperty)
        {
            if (assetProperty == null || assetProperty.AssetId <= 0) 
                throw new ArgumentException($"assetProperty");

            Asset asset = (await this.GetAssets(a => a.AssetId == assetProperty.AssetId))
                            .ToList()
                            .FirstOrDefault();

            if (asset == null)
            {
                asset = new Asset()
                {
                    AssetId = assetProperty.AssetId,
                    AssetName = $"Asset {assetProperty.AssetId}"
                };
                await _dbSetAsset.AddAsync(asset);                
            }                

            await _dbSetAssetProperty.AddAsync(assetProperty);
        }

        public async Task AddOrUpdateAssetProperty(AssetProperty assetProperty)
        {
            if (assetProperty == null) throw new ArgumentException($"assetProperty");

            var assetPropAtDB = (await this.GetAssetProperty(ap =>             
                ap.AssetId == assetProperty.AssetId && 
                ap.Property == assetProperty.Property
            )).ToList().FirstOrDefault();

            if (assetPropAtDB == null)
            {
                await this.InsertAssetProperty(assetProperty);
            } 
            else if(assetPropAtDB.Timestamp < assetProperty.Timestamp)
            {
                assetPropAtDB.Value = assetProperty.Value;
                assetPropAtDB.Timestamp = assetProperty.Timestamp;
                _dbSetAssetProperty.Update(assetPropAtDB);
            }                
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }
    }
}

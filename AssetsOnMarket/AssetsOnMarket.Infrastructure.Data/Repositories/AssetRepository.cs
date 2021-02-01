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
using Microsoft.Extensions.Logging;

namespace AssetsOnMarket.Infrastructure.Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetsOnMarketDBContext _context;
        private readonly DbSet<AssetProperty> _dbSetAssetProperty;
        private readonly DbSet<Asset> _dbSetAsset;
        private ILogger _logger;

        public AssetRepository(AssetsOnMarketDBContext context, ILogger<AssetRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException($"{context}");
            _dbSetAssetProperty = _context.Set<AssetProperty>();
            _dbSetAsset = _context.Set<Asset>();
            _logger = logger;
        }

        public IEnumerable<AssetProperty> GetAssetProperty(Expression<Func<AssetProperty, bool>> filter = null)
        {
            IQueryable<AssetProperty> queryable = _dbSetAssetProperty;
            return queryable.Where(filter).ToList();
        }

        public IEnumerable<Asset> GetAssets(Expression<Func<Asset, bool>> filter = null)
        {
            IQueryable<Asset> queryable = _dbSetAsset;
            return queryable.Where(filter).ToList();
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

            Asset asset = GetAssets(a => a.AssetId == assetProperty.AssetId)
                            .ToList()
                            .FirstOrDefault();

            if (asset == null)
            {
                asset = new Asset()
                {
                    AssetId = assetProperty.AssetId,
                    AssetName = $"Asset {assetProperty.AssetId}"
                };
                await InsertAsset(asset);                
            }                

            await _dbSetAssetProperty.AddAsync(assetProperty);
        }

        public async Task AddOrUpdateAssetProperty(AssetProperty assetProperty)
        {
            if (assetProperty == null) throw new ArgumentException($"assetProperty");

            var assetPropAtDB = GetAssetProperty(ap =>             
                                    ap.AssetId == assetProperty.AssetId && ap.Property == assetProperty.Property)
                                .ToList().FirstOrDefault();

            if (assetPropAtDB == null)
            {
                _logger.LogInformation($"AssetId: '{assetProperty.AssetId}' and Property: '{assetProperty.Property}', could not be found on the DB");
                await InsertAssetProperty(assetProperty);
            } 
            else if(assetPropAtDB.Timestamp < assetProperty.Timestamp)
            {
                assetPropAtDB.Value = assetProperty.Value;
                assetPropAtDB.Timestamp = assetProperty.Timestamp;
                _dbSetAssetProperty.Update(assetPropAtDB);
            }

        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
                
            }
        }
    }
}

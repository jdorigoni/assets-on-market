using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AssetsOnMarket.Domain.Interfaces;
using AssetsOnMarket.Domain.Models;
using AssetsOnMarket.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AssetsOnMarket.Infrastructure.Data.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly AssetsOnMarketDBContext _context;
        private readonly DbSet<AssetProperty> _dbSetAssetProperty;
        private readonly DbSet<Asset> _dbSetAsset;
        private ILogger _logger;

        public AssetRepository(AssetsOnMarketDBContext context, ILogger logger)
        {
            _context = context ?? throw new ArgumentNullException($"{context}");
            _dbSetAssetProperty = _context.Set<AssetProperty>();
            _dbSetAsset = _context.Set<Asset>();
            _logger = logger;
        }

        public IEnumerable<AssetProperty> GetAssetProperty(Expression<Func<AssetProperty, bool>> filter = null)
        {
            return _dbSetAssetProperty.Where(filter).ToList();
        }

        public IEnumerable<Asset> GetAssets(Expression<Func<Asset, bool>> filter = null)
        {
            return _dbSetAsset.Where(filter).ToList();
        }

        public IEnumerable<AssetProperty> GetAssetPropertyNoTracking(Expression<Func<AssetProperty, bool>> filter = null)
        {
            return _dbSetAssetProperty.AsNoTracking().Where(filter).ToList();
        }

        public IEnumerable<Asset> GetAssetsNoTracking(Expression<Func<Asset, bool>> filter = null)
        {
            return _dbSetAsset.AsNoTracking().Where(filter).ToList();
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

        public async Task<int> AddOrUpdateAssetProperty(AssetProperty assetProperty)
        {
            if (assetProperty == null) throw new ArgumentException($"assetProperty");

            var assetPropAtDB = GetAssetProperty(ap =>             
                                    ap.AssetId == assetProperty.AssetId && ap.Property == assetProperty.Property)
                                .ToList().FirstOrDefault();

            if (assetPropAtDB == null)
            {
                _logger.Information($"AssetId: '{assetProperty.AssetId}' and Property: '{assetProperty.Property}', could not be found on the DB");
                await InsertAssetProperty(assetProperty);
            } 
            else if(assetPropAtDB.Timestamp < assetProperty.Timestamp)
            {
                assetPropAtDB.Value = assetProperty.Value;
                assetPropAtDB.Timestamp = assetProperty.Timestamp;
                _dbSetAssetProperty.Update(assetPropAtDB);
            }

            _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT {typeof(Asset).Name} ON;");

            int numberOfChanges = SaveChanges();

            _context.Database.ExecuteSqlInterpolated($"SET IDENTITY_INSERT {typeof(Asset).Name} OFF;");

            return await Task.Run(() => numberOfChanges);
        }

        public Task BulkInsertUpdate(List<AssetProperty> assetsOnFile, int maxBatchSize)
        {
            try
            {
                var insertAssetRecords = new List<Asset>();
                var insertAssetPropRecords = new List<AssetProperty>();
                var updateAssetPropRecords = new List<AssetProperty>();

                foreach (var assetPropOnFile in assetsOnFile)
                {
                    var assetPropOnDB = GetAssetPropertyNoTracking(ap =>
                                            ap.AssetId == assetPropOnFile.AssetId &&
                                            ap.Property == assetPropOnFile.Property)
                                            .ToList()
                                            .FirstOrDefault();

                    if (assetPropOnDB != null)
                    {
                        if (assetPropOnDB.Timestamp < assetPropOnFile.Timestamp)
                        {
                            assetPropOnDB.Timestamp = assetPropOnFile.Timestamp;
                            assetPropOnDB.Value = assetPropOnFile.Value;
                            updateAssetPropRecords.Add(assetPropOnDB);
                        }
                    }
                    else
                    {
                        Log.Information($"Asset Id: '{assetPropOnFile.AssetId}' - " +
                                        $"Property: '{assetPropOnFile.Property}' was not found in the Database.");

                        var assetOnDb = GetAssetsNoTracking(a => a.AssetId == assetPropOnFile.AssetId)
                                            .ToList()
                                            .FirstOrDefault();
                        if (assetOnDb == null && 
                            insertAssetRecords.Find(a => a.AssetId == assetPropOnFile.AssetId) == null)
                        {    
                            insertAssetRecords.Add(new Asset()
                            {
                                AssetId = assetPropOnFile.AssetId,
                                AssetName = $"Asset {assetPropOnFile.AssetId}"
                            });                            
                        }

                        insertAssetPropRecords.Add(assetPropOnFile);
                    }
                }

                return Task.Run(() =>
                {
                    _context.BulkInsert(insertAssetRecords, bulk => bulk.BatchSize = maxBatchSize);
                    _context.BulkInsert(insertAssetPropRecords, bulk => bulk.BatchSize = maxBatchSize);
                    _context.BulkUpdate(updateAssetPropRecords, bulk => bulk.BatchSize = maxBatchSize);
                });
            }
            catch (Exception)
            {
                throw;
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

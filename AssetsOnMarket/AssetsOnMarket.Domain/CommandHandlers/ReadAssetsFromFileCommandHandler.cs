using AssetsOnMarket.Domain.Commands;
using AssetsOnMarket.Domain.Interfaces;
using AssetsOnMarket.Domain.Models;
using MediatR;
using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AssetsOnMarket.Domain.CommandHandlers
{
    public class ReadAssetsFromFileCommandHandler : IRequestHandler<ReadAssetsFromFileCommand, int>
    {
        private readonly IAssetRepository _assetRepository;

        public ReadAssetsFromFileCommandHandler(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<int> Handle(ReadAssetsFromFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<AssetProperty> assetsOnFile = ReadAssetsWithoutDuplicates();

                await _assetRepository.BulkInsertUpdate(assetsOnFile, request.MaxBatchSize);
                
                return await Task.FromResult(assetsOnFile.Count());
            }
            catch (Exception)
            {
                throw;                
            }
        }

        #region [Helper Private Methods]
        private static List<AssetProperty> ReadAssetsWithoutDuplicates()
        {
            List<AssetProperty> assetsOnFile = new List<AssetProperty>();

            string filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\assets\\assetsFile.csv";

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        AssetProperty assetPropertyLine = CreateFromReadLine(ref line);

                        var existingAssetProp = assetsOnFile.Find(a =>
                                                    a.AssetId == assetPropertyLine.AssetId &&
                                                    a.Property == assetPropertyLine.Property);

                        if (existingAssetProp != null)
                        {
                            if (existingAssetProp.Timestamp < assetPropertyLine.Timestamp)
                            {
                                assetsOnFile.Remove(existingAssetProp);
                                assetsOnFile.Add(assetPropertyLine);
                            }
                        }
                        else
                            assetsOnFile.Add(assetPropertyLine);
                    }
                }
            }

            return assetsOnFile;
        }

        private static AssetProperty CreateFromReadLine(ref string line)
        {
            line = line.Replace("\"", "");
            var values = line.Split(',');
            AssetProperty assetProperty = new AssetProperty()
            {
                AssetId = Convert.ToInt32(values[0]),
                Property = values[1].Trim(),
                Value = Boolean.Parse(values[2].Trim()),
                Timestamp = DateTime.ParseExact(values[3].Trim(), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)
            };
            return assetProperty;
        }

        #endregion
    }
}

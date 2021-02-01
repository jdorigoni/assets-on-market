using AssetsOnMarket.Domain.Commands;
using AssetsOnMarket.Domain.Interfaces;
using AssetsOnMarket.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public Task<int> Handle(ReadAssetsFromFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\assets\\assetsFile.csv";

                List<AssetProperty> assetsOnFile = new List<AssetProperty>();
                List<AssetProperty> insertRecords = new List<AssetProperty>();
                List<AssetProperty> updateRecords = new List<AssetProperty>();

                using (StreamReader reader = new StreamReader(filePath))
                { 
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line != null)
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
                            
                            assetsOnFile.Add(assetProperty);
                        }
                    }
                }              


                return Task.FromResult(1);
            }
            catch (Exception)
            {
                throw;                
            }
        }
    }
}

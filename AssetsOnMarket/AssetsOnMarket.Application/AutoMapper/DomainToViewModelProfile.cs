using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AssetsOnMarket.Application.ViewModels;
using AssetsOnMarket.Domain.Models;
using System.Globalization;

namespace AssetsOnMarket.Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<AssetProperty, AssetPropertyViewModel>()
                .ConstructUsing(ap => new AssetPropertyViewModel(
                                            ap.AssetId,
                                            ap.Property,
                                            ap.Value.ToString(),
                                            ap.Timestamp.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture))
                ); 
        }
    }
}

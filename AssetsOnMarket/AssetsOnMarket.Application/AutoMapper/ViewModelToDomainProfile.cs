using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AssetsOnMarket.Application.ViewModels;
using AssetsOnMarket.Domain.Commands;
using System.Globalization;
using AssetsOnMarket.Domain.Queries;

namespace AssetsOnMarket.Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<AssetPropertyViewModel, UpdateAssetPropertyCommand>()
                .ConstructUsing(vm => new UpdateAssetPropertyCommand(
                                            vm.AssetId, 
                                            vm.Property, 
                                            Boolean.Parse(vm.Value), 
                                            DateTime.ParseExact(vm.Timestamp.Trim(), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture))
                );

            CreateMap<PropertyValueViewModel, AssetsIdsByPropertyValueQuery>()
                .ConstructUsing(vm => new AssetsIdsByPropertyValueQuery(
                                            vm.Property,
                                            Boolean.Parse(vm.Value))
                );
        }
    }
}

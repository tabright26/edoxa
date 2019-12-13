// Filename: BalanceResponseProfile.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Cashier.Api.Application.Profiles
{
    internal sealed class BalanceResponseProfile : Profile
    {
        public BalanceResponseProfile()
        {
            this.CreateMap<Balance, BalanceDto>()
                .ForMember(balance => balance.Currency, config => config.MapFrom(balance => balance.Currency.ToEnum<CurrencyDto>()))
                .ForMember(balance => balance.Available, config => config.MapFrom<DecimalValue>(balance => balance.Available))
                .ForMember(balance => balance.Pending, config => config.MapFrom<DecimalValue>(balance => balance.Pending));
        }
    }
}

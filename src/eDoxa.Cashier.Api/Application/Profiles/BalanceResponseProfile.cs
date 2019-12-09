// Filename: BalanceResponseProfile.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Responses;

namespace eDoxa.Cashier.Api.Application.Profiles
{
    internal sealed class BalanceResponseProfile : Profile
    {
        public BalanceResponseProfile()
        {
            this.CreateMap<Balance, BalanceResponse>()
                .ForMember(balance => balance.Currency, config => config.MapFrom(balance => balance.Currency.Name))
                .ForMember(balance => balance.Available, config => config.MapFrom(balance => balance.Available))
                .ForMember(balance => balance.Pending, config => config.MapFrom(balance => balance.Pending));
        }
    }
}

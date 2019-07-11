// Filename: BalanceViewModelProfile.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Api.Profiles
{
    internal sealed class BalanceViewModelProfile : Profile
    {
        public BalanceViewModelProfile()
        {
            this.CreateMap<Balance, BalanceViewModel>()
                .ForMember(balance => balance.Currency, config => config.MapFrom(balance => balance.Currency))
                .ForMember(balance => balance.Available, config => config.MapFrom(balance => balance.Available))
                .ForMember(balance => balance.Pending, config => config.MapFrom(balance => balance.Pending));
        }
    }
}

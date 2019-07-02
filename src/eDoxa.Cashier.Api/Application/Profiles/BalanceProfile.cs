// Filename: BalanceProfile.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.ViewModels;

namespace eDoxa.Cashier.Api.Application.Profiles
{
    internal sealed class BalanceProfile : Profile
    {
        public BalanceProfile()
        {
            this.CreateMap<Balance, BalanceViewModel>()
                .ForMember(balance => balance.CurrencyType, config => config.MapFrom(balance => balance.CurrencyType))
                .ForMember(balance => balance.Available, config => config.MapFrom(balance => balance.Available))
                .ForMember(balance => balance.Pending, config => config.MapFrom(balance => balance.Pending));
        }
    }
}

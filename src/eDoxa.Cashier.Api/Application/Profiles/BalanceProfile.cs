// Filename: BalanceProfile.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright � 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Api.Application.Profiles
{
    internal sealed class BalanceProfile : Profile
    {
        public BalanceProfile()
        {
            this.CreateMap<Balance, BalanceViewModel>()
                .ForMember(balance => balance.Currency, config => config.MapFrom(balance => balance.Currency))
                .ForMember(balance => balance.Available, config => config.MapFrom(balance => balance.Available))
                .ForMember(balance => balance.Pending, config => config.MapFrom(balance => balance.Pending));
        }
    }
}

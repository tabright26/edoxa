// Filename: balanceProfile.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class BalanceProfile : Profile
    {
        public BalanceProfile()
        {
            this.CreateMap<Balance, BalanceDTO>()
                .ForMember(balance => balance.CurrencyType, config => config.MapFrom(balance => balance.CurrencyType))
                .ForMember(balance => balance.Available, config => config.MapFrom(balance => balance.Available))
                .ForMember(balance => balance.Pending, config => config.MapFrom(balance => balance.Pending));
        }
    }
}

// Filename: CurrencyProfile.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;

namespace eDoxa.Cashier.DTO.Profiles
{
    public sealed class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            this.CreateMap<MoneyAccount, CurrencyDTO>()
                .ForMember(currency => currency.Balance, config => config.MapFrom<decimal>(account => account.Balance))
                .ForMember(currency => currency.Pending, config => config.MapFrom<decimal>(account => account.Pending));

            this.CreateMap<TokenAccount, CurrencyDTO>()
                .ForMember(currency => currency.Balance, config => config.MapFrom<decimal>(account => account.Balance))
                .ForMember(currency => currency.Pending, config => config.MapFrom<decimal>(account => account.Pending));
        }
    }
}
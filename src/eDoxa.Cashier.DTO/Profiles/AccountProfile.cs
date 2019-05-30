// Filename: AccountProfile.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class AccountProfile : Profile
    {
        public AccountProfile()
        {
            this.CreateMap<MoneyAccount, AccountDTO>()
                .ForMember(account => account.Balance, config => config.MapFrom<decimal>(account => account.Balance))
                .ForMember(account => account.Pending, config => config.MapFrom<decimal>(account => account.Pending))
                .ForMember(account => account.CurrencyType, config => config.MapFrom(_ => CurrencyType.Money));

            this.CreateMap<TokenAccount, AccountDTO>()
                .ForMember(account => account.Balance, config => config.MapFrom<decimal>(account => account.Balance))
                .ForMember(account => account.Pending, config => config.MapFrom<decimal>(account => account.Pending))
                .ForMember(account => account.CurrencyType, config => config.MapFrom(_ => CurrencyType.Token));
        }
    }
}

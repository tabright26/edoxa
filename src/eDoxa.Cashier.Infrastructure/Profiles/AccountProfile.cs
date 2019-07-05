// Filename: AccountProfile.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Cashier.Infrastructure.Profiles.Converters;

namespace eDoxa.Cashier.Infrastructure.Profiles
{
    internal sealed class AccountProfile : Profile
    {
        public AccountProfile()
        {
            this.CreateMap<AccountModel, IAccount>().ConvertUsing(new AccountConverter());

            this.CreateMap<IAccount, AccountModel>()
                .ForMember(account => account.Id, config => config.MapFrom<Guid>(account => account.Id))
                .ForMember(account => account.UserId, config => config.MapFrom<Guid>(account => account.UserId))
                .ForMember(account => account.Transactions, config => config.MapFrom(account => account.Transactions));
        }
    }
}

﻿// Filename: AccountModelProfile.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Infrastructure.Models.Profiles
{
    internal sealed class AccountModelProfile : Profile
    {
        public AccountModelProfile()
        {
            this.CreateMap<IAccount, AccountModel>()
                .ForMember(account => account.Id, config => config.MapFrom<Guid>(account => account.Id))
                .ForMember(account => account.UserId, config => config.MapFrom<Guid>(account => account.UserId))
                .ForMember(account => account.Transactions, config => config.MapFrom(account => account.Transactions));
        }
    }
}
// Filename: AccountProfile.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Cashier.DTO.Profiles
{
    public sealed class AccountProfile : Profile
    {
        public AccountProfile()
        {
            this.CreateMap<Account, AccountDTO>()
                .ForMember(account => account.Funds, config => config.MapFrom(account => account.Funds))
                .ForMember(account => account.Tokens, config => config.MapFrom(account => account.Tokens));
        }
    }
}
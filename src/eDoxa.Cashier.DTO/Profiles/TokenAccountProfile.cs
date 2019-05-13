// Filename: TokenAccountProfile.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class TokenAccountProfile : Profile
    {
        public TokenAccountProfile()
        {
            this.CreateMap<TokenAccount, TokenAccountDTO>()
                .ForMember(account => account.Balance, config => config.MapFrom<long>(account => account.Balance))
                .ForMember(account => account.Pending, config => config.Ignore());
        }
    }
}
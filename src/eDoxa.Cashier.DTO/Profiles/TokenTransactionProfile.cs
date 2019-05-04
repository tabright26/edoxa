// Filename: TokenTransactionProfile.cs
// Date Created: 2019-04-26
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
    internal sealed class TokenTransactionProfile : Profile
    {
        public TokenTransactionProfile()
        {
            this.CreateMap<TokenTransaction, TokenTransactionDTO>()
                .ForMember(transaction => transaction.Id, config => config.MapFrom(transaction => transaction.Id.ToGuid()))
                .ForMember(transaction => transaction.Timestamp, config => config.MapFrom(transaction => transaction.Timestamp))
                .ForMember(transaction => transaction.Amount, config => config.MapFrom<long>(transaction => transaction.Amount));
        }
    }
}
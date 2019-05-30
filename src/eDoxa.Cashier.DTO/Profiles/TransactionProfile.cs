// Filename: TransactionProfile.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            this.CreateMap<Transaction, TransactionDTO>()
                .ForMember(transaction => transaction.Id, config => config.MapFrom(transaction => transaction.Id.ToGuid()))
                .ForMember(transaction => transaction.Timestamp, config => config.MapFrom(transaction => transaction.Timestamp))
                .ForMember(transaction => transaction.Amount, config => config.MapFrom(transaction => transaction.Currency.Amount))
                .ForMember(transaction => transaction.CurrencyType, config => config.MapFrom(transaction => transaction.Currency.Type))
                .ForMember(transaction => transaction.Description, config => config.MapFrom(transaction => transaction.Description))
                .ForMember(transaction => transaction.Type, config => config.MapFrom(transaction => transaction.Type))
                .ForMember(transaction => transaction.Status, config => config.MapFrom(transaction => transaction.Status));
        }
    }
}

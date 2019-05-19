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

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            this.CreateMap<MoneyTransaction, TransactionDTO>()
                .ForMember(transaction => transaction.Id, config => config.MapFrom(transaction => transaction.Id.ToGuid()))
                .ForMember(transaction => transaction.Timestamp, config => config.MapFrom(transaction => transaction.Timestamp))
                .ForMember(transaction => transaction.Amount, config => config.MapFrom<decimal>(transaction => transaction.Amount))
                .ForMember(transaction => transaction.Currency, config => config.MapFrom(_ => AccountCurrency.Money))
                .ForMember(transaction => transaction.Status, config => config.MapFrom(transaction => transaction.Status))
                .ForMember(transaction => transaction.Type, config => config.MapFrom(transaction => transaction.Type))
                .ForMember(transaction => transaction.Description, config => config.MapFrom(transaction => transaction.Description));

            this.CreateMap<TokenTransaction, TransactionDTO>()
                .ForMember(transaction => transaction.Id, config => config.MapFrom(transaction => transaction.Id.ToGuid()))
                .ForMember(transaction => transaction.Timestamp, config => config.MapFrom(transaction => transaction.Timestamp))
                .ForMember(transaction => transaction.Amount, config => config.MapFrom<decimal>(transaction => transaction.Amount))
                .ForMember(transaction => transaction.Currency, config => config.MapFrom(_ => AccountCurrency.Token))
                .ForMember(transaction => transaction.Status, config => config.MapFrom(transaction => transaction.Status))
                .ForMember(transaction => transaction.Type, config => config.MapFrom(transaction => transaction.Type))
                .ForMember(transaction => transaction.Description, config => config.MapFrom(transaction => transaction.Description));
        }
    }
}
// Filename: TransactionModelProfile.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

namespace eDoxa.Cashier.Infrastructure.Models.Profiles
{
    internal sealed class TransactionModelProfile : Profile
    {
        public TransactionModelProfile()
        {
            this.CreateMap<ITransaction, TransactionModel>()
                .ForMember(transaction => transaction.Id, config => config.MapFrom<Guid>(transaction => transaction.Id))
                .ForMember(transaction => transaction.Timestamp, config => config.MapFrom(transaction => transaction.Timestamp))
                .ForMember(transaction => transaction.Description, config => config.MapFrom(transaction => transaction.Description.Text))
                .ForMember(transaction => transaction.Amount, config => config.MapFrom(transaction => transaction.Currency.Amount))
                .ForMember(transaction => transaction.Currency, config => config.MapFrom(transaction => transaction.Currency.Type.Value))
                .ForMember(transaction => transaction.Type, config => config.MapFrom(transaction => transaction.Type.Value))
                .ForMember(transaction => transaction.Status, config => config.MapFrom(transaction => transaction.Status.Value))
                .ForMember(
                    transaction => transaction.Metadata,
                    config => config.MapFrom(
                        transaction => transaction.Metadata.Select(
                                metadata => new TransactionMetadataModel
                                {
                                    Key = metadata.Key,
                                    Value = metadata.Value
                                })
                            .ToList()))
                .ForMember(transaction => transaction.Account, config => config.Ignore());
        }
    }
}

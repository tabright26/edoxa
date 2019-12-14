// Filename: BalanceResponseProfile.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Domain.Extensions;

using Google.Protobuf.WellKnownTypes;

namespace eDoxa.Cashier.Api.Profiles
{
    internal sealed class AccountProfile : Profile
    {
        public AccountProfile()
        {
            this.CreateMap<Balance, BalanceDto>()
                .ForMember(balance => balance.Currency, config => config.MapFrom(balance => balance.Currency.ToEnum<CurrencyDto>()))
                .ForMember(balance => balance.Available, config => config.MapFrom<DecimalValue>(balance => balance.Available))
                .ForMember(balance => balance.Pending, config => config.MapFrom<DecimalValue>(balance => balance.Pending));

            this.CreateMap<Bundle, TransactionBundleDto>()
                .ForMember(balance => balance.Amount, config => config.MapFrom<DecimalValue>(balance => balance.Currency.Amount))
                .ForMember(balance => balance.Price, config => config.MapFrom<DecimalValue>(balance => balance.Price.Money.Amount));

            this.CreateMap<ITransaction, TransactionDto>()
                .ForMember(transaction => transaction.Id, config => config.MapFrom(transaction => transaction.Id.ToString()))
                .ForMember(transaction => transaction.Timestamp, config => config.MapFrom(transaction => DateTime.SpecifyKind(transaction.Timestamp, DateTimeKind.Utc).ToTimestamp()))
                .ForMember(transaction => transaction.Currency, config => config.MapFrom(transaction => transaction.Currency.Type.ToEnum<CurrencyDto>()))
                .ForMember(transaction => transaction.Amount, config => config.MapFrom<DecimalValue>(transaction => transaction.Currency.Amount))
                .ForMember(transaction => transaction.Type, config => config.MapFrom(transaction => transaction.Type.ToEnum<TransactionTypeDto>()))
                .ForMember(transaction => transaction.Status, config => config.MapFrom(transaction => transaction.Status.ToEnum<TransactionStatusDto>()))
                .ForMember(transaction => transaction.Description, config => config.MapFrom(transaction => transaction.Description.Text));
        }
    }
}

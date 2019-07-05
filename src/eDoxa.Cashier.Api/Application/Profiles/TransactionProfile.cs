// Filename: TransactionProfile.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels;

namespace eDoxa.Cashier.Api.Application.Profiles
{
    internal sealed class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            this.CreateMap<ITransaction, TransactionViewModel>()
                .ForMember(transaction => transaction.Id, config => config.MapFrom(transaction => transaction.Id.ToGuid()))
                .ForMember(transaction => transaction.Timestamp, config => config.MapFrom(transaction => transaction.Timestamp))
                .ForMember(transaction => transaction.Currency, config => config.MapFrom(transaction => transaction.Currency.Type))
                .ForMember(transaction => transaction.Amount, config => config.MapFrom(transaction => transaction.Currency.Amount))
                .ForMember(transaction => transaction.Type, config => config.MapFrom(transaction => transaction.Type))
                .ForMember(transaction => transaction.Status, config => config.MapFrom(transaction => transaction.Status))
                .ForMember(transaction => transaction.Description, config => config.MapFrom(transaction => transaction.Description));
        }
    }
}

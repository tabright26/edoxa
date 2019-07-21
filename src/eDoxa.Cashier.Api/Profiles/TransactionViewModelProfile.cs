// Filename: TransactionViewModelProfile.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

namespace eDoxa.Cashier.Api.Profiles
{
    internal sealed class TransactionViewModelProfile : Profile
    {
        public TransactionViewModelProfile()
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

// Filename: TransactionModelProfile.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

namespace eDoxa.Cashier.Infrastructure.Models.Profiles
{
    internal sealed class TransactionModelProfile : Profile
    {
        public TransactionModelProfile()
        {
            this.CreateMap<ITransaction, TransactionModel>()
                .ForMember(user => user.Id, config => config.MapFrom<Guid>(user => user.Id))
                .ForMember(user => user.Timestamp, config => config.MapFrom(user => user.Timestamp))
                .ForMember(user => user.Description, config => config.MapFrom(user => user.Description.Text))
                .ForMember(user => user.Amount, config => config.MapFrom(user => user.Currency.Amount))
                .ForMember(user => user.Currency, config => config.MapFrom(user => user.Currency.Type.Value))
                .ForMember(user => user.Type, config => config.MapFrom(user => user.Type.Value))
                .ForMember(user => user.Status, config => config.MapFrom(user => user.Status.Value))
                .ForMember(user => user.Account, config => config.Ignore());
        }
    }
}

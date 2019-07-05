// Filename: TransactionProfile.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Cashier.Infrastructure.Profiles.Converters;

namespace eDoxa.Cashier.Infrastructure.Profiles
{
    internal sealed class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            this.CreateMap<TransactionModel, ITransaction>().ConvertUsing(new TransactionConverter());

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

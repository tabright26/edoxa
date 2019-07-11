// Filename: TransactionProfile.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Cashier.Infrastructure.Profiles.Converters;

namespace eDoxa.Cashier.Infrastructure.Profiles
{
    internal sealed class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            this.CreateMap<TransactionModel, ITransaction>().ConvertUsing(new TransactionConverter());
        }
    }
}

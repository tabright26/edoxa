// Filename: TransactionListProfile.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class TransactionListProfile : Profile
    {
        public TransactionListProfile()
        {
            this.CreateMap<IEnumerable<Transaction>, TransactionListDTO>()
                .ForMember(list => list.Items, config => config.MapFrom(transactions => transactions.ToList()));
        }
    }
}

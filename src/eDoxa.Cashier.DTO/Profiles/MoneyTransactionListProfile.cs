// Filename: MoneyTransactionListProfile.cs
// Date Created: 2019-04-26
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

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Cashier.DTO.Profiles
{
    internal sealed class MoneyTransactionListProfile : Profile
    {
        public MoneyTransactionListProfile()
        {
            this.CreateMap<IEnumerable<MoneyTransaction>, MoneyTransactionListDTO>()
                .ForMember(list => list.Items, config => config.MapFrom(transactions => transactions.ToList()));
        }
    }
}
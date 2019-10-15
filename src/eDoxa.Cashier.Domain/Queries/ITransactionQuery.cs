﻿// Filename: ITransactionQuery.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface ITransactionQuery
    {
        IMapper Mapper { get; }

        Task<IReadOnlyCollection<ITransaction>> FindUserTransactionsAsync(
            UserId userId,
            Currency? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        );

        Task<IReadOnlyCollection<ITransaction>> FindUserTransactionsAsync(
            Currency? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        );
    }
}

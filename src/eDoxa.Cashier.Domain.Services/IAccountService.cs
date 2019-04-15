// Filename: IAccountService.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IAccountService
    {
        Task TransactionAsync<TCurrency>(CustomerId customerId, CurrencyBundle<TCurrency> bundle, CancellationToken cancellationToken = default)
        where TCurrency : Currency<TCurrency>, new();
    }
}
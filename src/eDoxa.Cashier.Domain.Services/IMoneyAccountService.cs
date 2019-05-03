// Filename: IAccountService.cs
// Date Created: 2019-04-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IMoneyAccountService
    {
        Task<IMoneyTransaction> TransactionAsync(UserId userId, CustomerId customerId, MoneyBundle bundle, CancellationToken cancellationToken = default);

        Task<Option<IMoneyTransaction>> TryWithdrawAsync(UserId userId, decimal amount, CancellationToken cancellationToken = default);
    }
}
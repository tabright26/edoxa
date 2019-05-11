// Filename: IMoneyAccountService.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IMoneyAccountService
    {
        Task<IMoneyTransaction> DepositAsync(UserId userId, CustomerId customerId, MoneyBundle bundle, CancellationToken cancellationToken = default);

        Task<Option<IMoneyTransaction>> TryWithdrawalAsync(UserId userId, CustomerId customerId, MoneyBundle bundle, CancellationToken cancellationToken = default);
    }
}
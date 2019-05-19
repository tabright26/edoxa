// Filename: IMoneyAccountService.cs
// Date Created: 2019-05-13
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
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Cashier.Domain.Services.Abstractions
{
    public interface IMoneyAccountService
    {
        Task CreateAccount(UserId userId);

        Task<Either<ValidationError, TransactionStatus>> DepositAsync(
            UserId userId,
            MoneyBundle bundle,
            StripeCustomerId customerId,
            CancellationToken cancellationToken = default
        );

        Task<Either<ValidationError, TransactionStatus>> WithdrawAsync(
            UserId userId,
            MoneyBundle bundle,
            StripeAccountId accountId,
            CancellationToken cancellationToken = default
        );
    }
}

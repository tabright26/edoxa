// Filename: IMoneyAccountService.cs
// Date Created: 2019-05-20
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
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Common;

using FluentValidation.Results;

namespace eDoxa.Cashier.Services.Abstractions
{
    public interface IMoneyAccountService
    {
        Task<Either<ValidationResult, TransactionStatus>> DepositAsync(
            UserId userId,
            MoneyBundle bundle,
            StripeCustomerId customerId,
            CancellationToken cancellationToken = default
        );

        Task<Either<ValidationResult, TransactionStatus>> WithdrawAsync(
            UserId userId,
            MoneyBundle bundle,
            StripeAccountId accountId,
            CancellationToken cancellationToken = default
        );
    }
}

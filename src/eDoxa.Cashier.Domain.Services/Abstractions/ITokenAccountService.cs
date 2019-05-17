// Filename: ITokenAccountService.cs
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
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;

namespace eDoxa.Cashier.Domain.Services.Abstractions
{
    public interface ITokenAccountService
    {
        Task<Either<TransactionStatus>> DepositAsync(
            UserId userId,
            StripeCustomerId customerId,
            TokenBundle bundle,
            CancellationToken cancellationToken = default);

        Task CreateAccount(UserId userId);
    }
}
// Filename: IMoneyAccountService.cs
// Date Created: 2019-05-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;

namespace eDoxa.Cashier.Domain.Services.Abstractions
{
    public interface IMoneyAccountService
    {
        Task<Either<ValidationResult, IMoneyTransaction>> DepositAsync(UserId userId, CustomerId customerId, MoneyBundle bundle, string email, CancellationToken cancellationToken = default);

        Task<Either<ValidationResult, IMoneyTransaction>> TryWithdrawalAsync(
            UserId userId,
            CustomerId customerId,
            MoneyBundle bundle,
            CancellationToken cancellationToken = default);
    }
}
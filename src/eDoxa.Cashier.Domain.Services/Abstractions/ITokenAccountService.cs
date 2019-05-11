// Filename: ITokenAccountService.cs
// Date Created: 2019-05-11
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
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services.Stripe.Models;

namespace eDoxa.Cashier.Domain.Services.Abstractions
{
    public interface ITokenAccountService
    {
        Task<ITokenTransaction> DepositAsync(UserId userId, CustomerId customerId, TokenBundle bundle, CancellationToken cancellationToken = default);
    }
}
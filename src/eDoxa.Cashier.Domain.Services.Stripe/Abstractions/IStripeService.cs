// Filename: IStripeService.cs
// Date Created: 2019-05-10
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
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services.Stripe.Models;

namespace eDoxa.Cashier.Domain.Services.Stripe.Abstractions
{
    public interface IStripeService
    {
        Task CreateInvoiceAsync(CustomerId customerId, MoneyBundle bundle, IMoneyTransaction transaction, CancellationToken cancellationToken = default);

        Task CreateInvoiceAsync(CustomerId customerId, TokenBundle bundle, ITokenTransaction transaction, CancellationToken cancellationToken = default);
    }
}
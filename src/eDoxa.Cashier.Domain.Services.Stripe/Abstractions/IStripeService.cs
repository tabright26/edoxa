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
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Models;

namespace eDoxa.Cashier.Domain.Services.Stripe.Abstractions
{
    public interface IStripeService
    {
        Task<BankAccountId> CreateBankAccountAsync(CustomerId customerId, string sourceToken, CancellationToken cancellationToken = default);

        Task DeleteBankAccountAsync(CustomerId customerId, BankAccountId bankAccountId, CancellationToken cancellationToken = default);

        Task CreateCardAsync(CustomerId customerId, string sourceToken, bool defaultSource, CancellationToken cancellationToken = default);

        Task DeleteCardAsync(CustomerId customerId, CardId cardId, CancellationToken cancellationToken = default);

        Task<CustomerId> CreateCustomerAsync(UserId userId, string email, CancellationToken cancellationToken = default);

        Task UpdateCustomerEmailAsync(CustomerId customerId, string email, CancellationToken cancellationToken = default);

        Task UpdateCustomerDefaultSourceAsync(CustomerId customerId, CardId cardId, CancellationToken cancellationToken = default);

        Task CreateInvoiceAsync(CustomerId customerId, IBundle bundle, ITransaction transaction, CancellationToken cancellationToken = default);

        Task CreatePayoutAsync(CustomerId customerId, IBundle bundle, ITransaction transaction, CancellationToken cancellationToken = default);
    }
}
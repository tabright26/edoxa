// Filename: IStripeService.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Stripe.Models;

using Stripe;

namespace eDoxa.Stripe.Abstractions
{
    public interface IStripeService
    {
        Task<StripeConnectAccountId> CreateAccountAsync(
            Guid userId,
            string email,
            string firstName,
            string lastName,
            int year,
            int month,
            int day,
            CancellationToken cancellationToken = default
        );

        Task VerifyAccountAsync(
            StripeConnectAccountId connectAccountId,
            string line1,
            string line2,
            string city,
            string state,
            string postalCode,
            CancellationToken cancellationToken = default
        );

        Task<StripeBankAccountId> CreateBankAccountAsync(
            StripeConnectAccountId connectAccountId,
            string externalAccountTokenId,
            CancellationToken cancellationToken = default
        );

        Task DeleteBankAccountAsync(StripeConnectAccountId connectAccountId, StripeBankAccountId bankAccountId, CancellationToken cancellationToken = default);

        Task<IEnumerable<Card>> GetCardsAsync(StripeCustomerId customerId);

        Task CreateCardAsync(StripeCustomerId customerId, string sourceToken, CancellationToken cancellationToken = default);

        Task DeleteCardAsync(StripeCustomerId customerId, StripeCardId cardId, CancellationToken cancellationToken = default);

        Task<Customer> GetCustomerAsync(StripeCustomerId customerId, CancellationToken cancellationToken = default);

        Task<StripeCustomerId> CreateCustomerAsync(
            Guid userId,
            StripeConnectAccountId connectAccountId,
            string email,
            CancellationToken cancellationToken = default
        );

        Task UpdateCardDefaultAsync(StripeCustomerId customerId, StripeCardId cardId, CancellationToken cancellationToken = default);

        Task CreateInvoiceAsync(
            StripeCustomerId customerId,
            long price,
            Guid transactionId,
            string transactionDescription,
            CancellationToken cancellationToken = default
        );

        Task CreateTransferAsync(
            StripeConnectAccountId connectAccountId,
            long price,
            Guid transactionId,
            string transactionDescription,
            CancellationToken cancellationToken = default
        );
    }
}

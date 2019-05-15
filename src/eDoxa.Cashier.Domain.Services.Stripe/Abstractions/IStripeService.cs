﻿// Filename: IStripeService.cs
// Date Created: 2019-05-10
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
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe.Abstractions
{
    public interface IStripeService
    {
        Task<StripeAccountId> CreateAccountAsync(
            UserId userId,
            string email,
            string firstName,
            string lastName,
            int year,
            int month,
            int day,
            CancellationToken cancellationToken = default);

        Task VerifyAccountAsync(
            StripeAccountId accountId,
            string line1,
            string line2,
            string city,
            string state,
            string postalCode,
            CancellationToken cancellationToken = default);

        Task<StripeBankAccountId> CreateBankAccountAsync(StripeAccountId accountId, string externalAccountTokenId, CancellationToken cancellationToken = default);

        Task DeleteBankAccountAsync(StripeAccountId accountId, StripeBankAccountId bankAccountId, CancellationToken cancellationToken = default);

        Task<Option<StripeList<Card>>> ListCardsAsync(StripeCustomerId customerId);

        Task<Option<Card>> GetCardAsync(StripeCustomerId customerId, StripeCardId cardId);

        Task<Either<ValidationResult, Card>> CreateCardAsync(
            StripeCustomerId customerId,
            string sourceToken,
            bool defaultSource,
            CancellationToken cancellationToken = default);

        Task<Either<ValidationResult, Card>> DeleteCardAsync(StripeCustomerId customerId, StripeCardId cardId, CancellationToken cancellationToken = default);

        Task<StripeCustomerId> CreateCustomerAsync(StripeAccountId accountId, UserId userId, string email, CancellationToken cancellationToken = default);

        Task<Either<ValidationResult, Customer>> UpdateCustomerDefaultSourceAsync(
            StripeCustomerId customerId,
            StripeCardId cardId,
            CancellationToken cancellationToken = default);

        Task<Either<ValidationResult, Invoice>> CreateInvoiceAsync(
            StripeCustomerId customerId,
            string email,
            IBundle bundle,
            ITransaction transaction,
            CancellationToken cancellationToken = default);

        Task CreateTransfer(StripeAccountId accountId, IBundle bundle, ITransaction transaction, CancellationToken cancellationToken = default);
    }
}
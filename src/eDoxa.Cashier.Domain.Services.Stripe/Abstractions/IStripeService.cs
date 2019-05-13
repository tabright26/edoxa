// Filename: IStripeService.cs
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
        Task<Option<BankAccount>> GetUserBankAccountAsync(CustomerId customerId);

        Task<Either<ValidationResult, BankAccount>> CreateBankAccountAsync(
            CustomerId customerId,
            string sourceToken,
            CancellationToken cancellationToken = default);

        Task<Either<ValidationResult, BankAccount>> DeleteBankAccountAsync(CustomerId customerId, CancellationToken cancellationToken = default);

        Task<Option<StripeList<Card>>> ListCardsAsync(CustomerId customerId);

        Task<Option<Card>> GetCardAsync(CustomerId customerId, CardId cardId);

        Task<Either<ValidationResult, Card>> CreateCardAsync(
            CustomerId customerId,
            string sourceToken,
            bool defaultSource,
            CancellationToken cancellationToken = default);

        Task<Either<ValidationResult, Card>> DeleteCardAsync(CustomerId customerId, CardId cardId, CancellationToken cancellationToken = default);

        Task<Either<ValidationResult, Customer>> CreateCustomerAsync(UserId userId, string email, CancellationToken cancellationToken = default);

        Task<Either<ValidationResult, Customer>> UpdateCustomerDefaultSourceAsync(
            CustomerId customerId,
            CardId cardId,
            CancellationToken cancellationToken = default);

        Task<Either<ValidationResult, Invoice>> CreateInvoiceAsync(
            CustomerId customerId,
            string email,
            IBundle bundle,
            ITransaction transaction,
            CancellationToken cancellationToken = default);
    }
}
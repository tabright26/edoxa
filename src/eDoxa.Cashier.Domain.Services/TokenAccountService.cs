// Filename: TokenAccountService.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Cashier.Domain.Services
{
    public sealed class TokenAccountService : ITokenAccountService
    {
        private readonly IStripeService _stripeService;
        private readonly ITokenAccountRepository _tokenAccountRepository;

        public TokenAccountService(ITokenAccountRepository tokenAccountRepository, IStripeService stripeService)
        {
            _tokenAccountRepository = tokenAccountRepository;
            _stripeService = stripeService;
        }

        public async Task CreateAccount(UserId userId)
        {
            _tokenAccountRepository.Create(new TokenAccount(userId));

            await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync();
        }

        public async Task<Either<ValidationError, TransactionStatus>> DepositAsync(
            UserId userId,
            TokenBundle bundle,
            StripeCustomerId customerId,
            CancellationToken cancellationToken = default)
        {
            var account = await _tokenAccountRepository.FindUserAccountAsync(userId);

            var result = account.CanDeposit();

            if (result.Failure)
            {
                return result.ValidationError;
            }

            var customer = await _stripeService.GetCustomerAsync(customerId, cancellationToken);

            if (customer.DefaultSource == null)
            {
                return new ValidationError("There are no credit cards associated with this account.");
            }

            var transaction = account.Deposit(bundle.Amount);

            await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(customerId, bundle, transaction, cancellationToken);

                transaction = account.CompleteTransaction(transaction);

                await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction.Status;
            }
            catch (Exception exception)
            {
                transaction = account.FailureTransaction(transaction, exception.Message);

                await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return new ValidationError(transaction.Failure.ToString());
            }
        }
    }
}
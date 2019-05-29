// Filename: TokenAccountService.cs
// Date Created: 2019-05-20
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

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Validators;
using eDoxa.Cashier.Services.Abstractions;
using eDoxa.Cashier.Services.Stripe.Abstractions;
using eDoxa.Functional;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Common;

using FluentValidation.Results;

namespace eDoxa.Cashier.Services
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

        public async Task<Either<ValidationResult, TransactionStatus>> DepositAsync(
            UserId userId,
            TokenBundle bundle,
            StripeCustomerId customerId,
            CancellationToken cancellationToken = default
        )
        {
            var account = await _tokenAccountRepository.GetUserAccountAsync(userId);

            var validator = new DepositTokenValidator();

            var result = validator.Validate(account);

            if (!result.IsValid)
            {
                return result;
            }

            var customer = await _stripeService.GetCustomerAsync(customerId, cancellationToken);

            if (customer.DefaultSource == null)
            {
                return new ValidationFailure(null, "There are no credit cards associated with this account.").ToResult();
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

                return new ValidationFailure(null, transaction.Failure.ToString()).ToResult();
            }
        }
    }
}

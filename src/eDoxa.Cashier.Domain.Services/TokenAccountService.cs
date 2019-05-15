﻿// Filename: TokenAccountService.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;

using Stripe;

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

        public async Task<Either<ValidationResult, ITokenTransaction>> DepositAsync(
            UserId userId,
            StripeCustomerId customerId,
            TokenBundle bundle,
            string email,
            CancellationToken cancellationToken = default)
        {
            var account = await _tokenAccountRepository.FindUserAccountAsync(userId);

            var transaction = account.Deposit(bundle.Amount);

            await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(customerId, bundle, transaction, cancellationToken);

                transaction.Pay();

                await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
            }
            catch (StripeException exception)
            {
                transaction.Fail();

                await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                ExceptionDispatchInfo.Capture(exception).Throw();

                throw;
            }

            return new Either<ValidationResult, ITokenTransaction>(transaction);
        }
    }
}
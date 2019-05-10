// Filename: TokenAccountService.cs
// Date Created: 2019-05-06
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

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;

namespace eDoxa.Cashier.Application.Services
{
    internal sealed class TokenAccountService : ITokenAccountService
    {
        private readonly IStripeService _stripeService;
        private readonly ITokenAccountRepository _tokenAccountRepository;

        public TokenAccountService(ITokenAccountRepository tokenAccountRepository, IStripeService stripeService)
        {
            _tokenAccountRepository = tokenAccountRepository;
            _stripeService = stripeService;
        }

        public async Task<ITokenTransaction> TransactionAsync(
            UserId userId,
            CustomerId customerId,
            TokenBundle bundle,
            CancellationToken cancellationToken = default)
        {
            var account = await _tokenAccountRepository.FindUserAccountAsync(userId);

            var transaction = account.Deposit(bundle.Amount);

            await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            try
            {
                await _stripeService.CreateInvoiceAsync(customerId, bundle, transaction, cancellationToken);
            }
            catch (Exception)
            {
                transaction.Fail();

                await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

                return transaction;
            }

            transaction.Pay();

            await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return transaction;
        }
    }
}
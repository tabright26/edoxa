// Filename: TokenAccountService.cs
// Date Created: 2019-04-30
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
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Services.Stripe;
using eDoxa.Cashier.Domain.Services.Stripe.Validators;

using Stripe;

namespace eDoxa.Cashier.Application.Services
{
    internal sealed class TokenAccountService : ITokenAccountService
    {
        private readonly CustomerService _customerService;
        private readonly InvoiceItemService _invoiceItemService;
        private readonly InvoiceService _invoiceService;

        private readonly ITokenAccountRepository _tokenAccountRepository;

        public TokenAccountService(
            ITokenAccountRepository tokenAccountRepository,
            CustomerService customerService,
            InvoiceService invoiceService,
            InvoiceItemService invoiceItemService)
        {
            _tokenAccountRepository = tokenAccountRepository;
            _customerService = customerService;
            _customerService.ExpandDefaultSource = true;
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
        }

        public async Task<ITokenTransaction> TransactionAsync(
            UserId userId,
            CustomerId customerId,
            TokenBundle bundle,
            CancellationToken cancellationToken = default)
        {
            var customer = await _customerService.GetAsync(customerId.ToString(), cancellationToken: cancellationToken);

            var validator = new CustomerDefaultSourceValidator();

            validator.Validate(customer);

            await _invoiceItemService.CreateAsync(InvoiceItemCreateOptions(customerId, bundle), cancellationToken: cancellationToken);

            await _invoiceService.CreateAsync(InvoiceCreateOptions(customer), cancellationToken: cancellationToken);

            var account = await _tokenAccountRepository.FindUserAccountAsync(userId);

            var transaction = account.Deposit(bundle.Amount);

            await _tokenAccountRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return transaction;
        }

        private static InvoiceItemCreateOptions InvoiceItemCreateOptions(CustomerId customerId, TokenBundle bundle)
        {
            return new InvoiceItemCreateOptions
            {
                CustomerId = customerId.ToString(),
                Description = "eDoxa (Tokens)",
                Amount = bundle.Price.AsCents(),
                Currency = "usd"
            };
        }

        private static InvoiceCreateOptions InvoiceCreateOptions(Customer customer)
        {
            return new InvoiceCreateOptions
            {
                CustomerId = customer.Id,
                TaxPercent = 15M,
                AutoAdvance = true,
                DefaultSource = customer.DefaultSource.Id
            };
        }
    }
}
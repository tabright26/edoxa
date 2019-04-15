// Filename: AccountService.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

using Stripe;

namespace eDoxa.Cashier.Application.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly CustomerService _customerService;
        private readonly InvoiceService _invoiceService;
        private readonly InvoiceItemService _invoiceItemService;

        public AccountService(CustomerService customerService, InvoiceService invoiceService, InvoiceItemService invoiceItemService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _invoiceService = invoiceService ?? throw new ArgumentNullException(nameof(invoiceService));
            _invoiceItemService = invoiceItemService ?? throw new ArgumentNullException(nameof(invoiceItemService));
        }

        public async Task TransactionAsync<TCurrency>(CustomerId customerId, CurrencyBundle<TCurrency> bundle, CancellationToken cancellationToken = default)
        where TCurrency : Currency<TCurrency>, new()
        {
            _customerService.ExpandDefaultSource = true;

            var customer = await _customerService.GetAsync(customerId.ToString(), cancellationToken: cancellationToken);

            if (customer.DefaultSource == null)
            {
                throw new InvalidOperationException("The customer default source payment is invalid. This customer doesn't have any default payment source.");
            }

            if (customer.DefaultSource.Object != "card")
            {
                throw new InvalidOperationException("The customer default source payment is invalid. Only credit card are accepted.");
            }

            var invoiceItem = bundle.BuildInvoiceItem(customerId);

            await _invoiceItemService.CreateAsync(invoiceItem, cancellationToken: cancellationToken);

            var invoiceOptions = new InvoiceCreateOptions
            {
                CustomerId = customerId.ToString(),
                TaxPercent = 15M,
                AutoAdvance = true,
                DefaultSource = customer.DefaultSource.Id
            };

            await _invoiceService.CreateAsync(invoiceOptions, cancellationToken: cancellationToken);
        }
    }
}
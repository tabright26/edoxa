// Filename: AccountService.cs
// Date Created: 2019-04-15
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Adapters;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Stripe.Validators;

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
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
            _customerService = customerService;
            _customerService.ExpandDefaultSource = true;
        }

        public async Task TransactionAsync<TCurrency>(CustomerId customerId, CurrencyBundle<TCurrency> bundle, CancellationToken cancellationToken = default)
        where TCurrency : Currency<TCurrency>, new()
        {
            var customer = await _customerService.GetAsync(customerId.ToString(), cancellationToken: cancellationToken);

            var validator = new CustomerDefaultSourceValidator();

            validator.Validate(customer);

            var transaction = bundle.CreateTransaction(customerId);

            await _invoiceItemService.CreateAsync(InvoiceItemCreateOptions(transaction), cancellationToken: cancellationToken);

            await _invoiceService.CreateAsync(InvoiceCreateOptions(customer), cancellationToken: cancellationToken);
        }

        private static InvoiceItemCreateOptions InvoiceItemCreateOptions(Transaction transaction)
        {
            var adapter = new InvoiceItemCreateOptionsAdapter(transaction);

            return adapter.InvoiceItemCreateOptions;
        }

        private static InvoiceCreateOptions InvoiceCreateOptions(Customer customer)
        {
            return new InvoiceCreateOptions
            {
                CustomerId = customer.Id, TaxPercent = Transaction.TaxPercent, AutoAdvance = true, DefaultSource = customer.DefaultSource.Id
            };
        }
    }
}
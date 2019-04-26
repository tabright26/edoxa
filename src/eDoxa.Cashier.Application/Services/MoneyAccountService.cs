// Filename: AccountService.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Stripe.Validators;

using Stripe;

namespace eDoxa.Cashier.Application.Services
{
    public sealed class MoneyAccountService : IMoneyAccountService
    {
        private readonly CustomerService _customerService;
        private readonly InvoiceItemService _invoiceItemService;
        private readonly InvoiceService _invoiceService;

        public MoneyAccountService(CustomerService customerService, InvoiceService invoiceService, InvoiceItemService invoiceItemService)
        {
            _customerService = customerService;
            _customerService.ExpandDefaultSource = true;
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
        }

        public async Task TransactionAsync<TCurrency>(User user, Bundle<TCurrency> bundle, CancellationToken cancellationToken = default)
        where TCurrency : ICurrency
        {
            var customer = await _customerService.GetAsync(user.CustomerId.ToString(), cancellationToken: cancellationToken);

            var validator = new CustomerDefaultSourceValidator();

            validator.Validate(customer);

            await _invoiceItemService.CreateAsync(InvoiceItemCreateOptions(user, bundle), cancellationToken: cancellationToken);

            await _invoiceService.CreateAsync(InvoiceCreateOptions(customer), cancellationToken: cancellationToken);
        }

        private static InvoiceItemCreateOptions InvoiceItemCreateOptions<TCurrency>(User user, Bundle<TCurrency> bundle)
        where TCurrency : ICurrency
        {
            return new InvoiceItemCreateOptions
            {
                CustomerId = user.CustomerId.ToString(),
                Description = "eDoxa",
                Amount = bundle.Price.AsCents(),
                Currency = "usd"
            };
        }

        private static InvoiceCreateOptions InvoiceCreateOptions(Customer customer)
        {
            return new InvoiceCreateOptions
            {
                CustomerId = customer.Id, TaxPercent = 15M, AutoAdvance = true, DefaultSource = customer.DefaultSource.Id
            };
        }
    }
}
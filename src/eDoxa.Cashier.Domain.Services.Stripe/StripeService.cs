// Filename: StripeService.cs
// Date Created: 2019-05-10
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
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Domain.Services.Stripe.Validators;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe
{
    public sealed class StripeService : IStripeService
    {
        private readonly CustomerService _customerService;
        private readonly InvoiceItemService _invoiceItemService;
        private readonly InvoiceService _invoiceService;

        public StripeService(CustomerService customerService, InvoiceService invoiceService, InvoiceItemService invoiceItemService)
        {
            _customerService = customerService;
            _customerService.ExpandDefaultSource = true;
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
        }

        public async Task CreateInvoiceAsync(
            CustomerId customerId,
            MoneyBundle bundle,
            IMoneyTransaction transaction,
            CancellationToken cancellationToken = default)
        {
            await this.CreateInvoiceAsync(customerId, bundle.Price, transaction.Description, cancellationToken);
        }

        public async Task CreateInvoiceAsync(
            CustomerId customerId,
            TokenBundle bundle,
            ITokenTransaction transaction,
            CancellationToken cancellationToken = default)
        {
            await this.CreateInvoiceAsync(customerId, bundle.Price, transaction.Description, cancellationToken);
        }

        public async Task CreateInvoiceAsync(
            CustomerId customerId,
            Money prize,
            TransactionDescription description,
            CancellationToken cancellationToken = default)
        {
            var customer = await _customerService.GetAsync(customerId.ToString(), cancellationToken: cancellationToken);

            var validator = new CustomerDefaultSourceValidator();

            validator.Validate(customer);

            await _invoiceItemService.CreateAsync(new InvoiceItemCreateOptions
            {
                CustomerId = customerId.ToString(),
                Description = description.ToString(),
                Amount = prize.AsCents(),
                Currency = "usd"
            }, cancellationToken: cancellationToken);

            await _invoiceService.CreateAsync(new InvoiceCreateOptions
            {
                CustomerId = customer.Id,
                TaxPercent = 15M,
                AutoAdvance = true,
                DefaultSource = customer.DefaultSource.Id
            }, cancellationToken: cancellationToken);
        }
    }
}
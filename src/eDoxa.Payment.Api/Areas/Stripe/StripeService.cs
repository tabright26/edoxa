// Filename: StripeService.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Payment.Api.Areas.Stripe.Abstractions;

using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe
{
    internal sealed class StripeService : IStripeService
    {
        private readonly StripeOptions _stripeOptions;
        private readonly AccountService _accountService;
        private readonly CustomerService _customerService;
        private readonly InvoiceItemService _invoiceItemService;
        private readonly InvoiceService _invoiceService;
        private readonly TransferService _transferService;

        public StripeService(
            IOptionsSnapshot<StripeOptions> options,
            AccountService accountService,
            CustomerService customerService,
            InvoiceService invoiceService,
            InvoiceItemService invoiceItemService,
            TransferService transferService
        )
        {
            _stripeOptions = options.Value;
            _accountService = accountService;
            _customerService = customerService;
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
            _transferService = transferService;
        }

        public async Task<string> CreateAccountAsync(
            Guid userId,
            string email,
            string firstName,
            string lastName,
            int year,
            int month,
            int day,
            CancellationToken cancellationToken = default
        )
        {
            var account = await _accountService.CreateAsync(
                new AccountCreateOptions
                {
                    Individual = new PersonCreateOptions
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Dob = new DobOptions
                        {
                            Day = day,
                            Month = month,
                            Year = year
                        }
                    },
                    Email = email,
                    Country = _stripeOptions.Country,
                    DefaultCurrency = _stripeOptions.Currency,
                    BusinessType = _stripeOptions.BusinessType,
                    Type = _stripeOptions.AccountType,
                    Metadata = new Dictionary<string, string>
                    {
                        ["userId"] = userId.ToString()
                    }
                },
                cancellationToken: cancellationToken);

            return account.Id;
        }

        public async Task<string> CreateCustomerAsync(
            Guid userId,
            string connectAccountId,
            string email,
            CancellationToken cancellationToken = default
        )
        {
            var customer = await _customerService.CreateAsync(
                new CustomerCreateOptions
                {
                    Email = email,
                    Metadata = new Dictionary<string, string>
                    {
                        ["userId"] = userId.ToString()
                    }
                },
                cancellationToken: cancellationToken);

            return customer.Id;
        }

        public async Task CreateTransferAsync(
            Guid transactionId,
            string transactionDescription,
            string connectAccountId,
            long amount,
            CancellationToken cancellationToken = default
        )
        {
            var options = new TransferCreateOptions
            {
                Destination = connectAccountId,
                Currency = _stripeOptions.Currency,
                Amount = amount,
                Description = transactionDescription,
                Metadata = new Dictionary<string, string>
                {
                    ["transactionId"] = transactionId.ToString()
                }
            };

            await _transferService.CreateAsync(options, cancellationToken: cancellationToken);
        }

        public async Task CreateInvoiceAsync(
            Guid transactionId,
            string transactionDescription,
            string customerId,
            long amount,
            CancellationToken cancellationToken = default
        )
        {
            await this.CreateInvoiceItemAsync(
                transactionId,
                transactionDescription,
                customerId,
                amount,
                cancellationToken);

            await this.CreateInvoiceAsync(transactionId, customerId, cancellationToken);
        }

        private async Task CreateInvoiceItemAsync(
            Guid transactionId,
            string transactionDescription,
            string customerId,
            long amount,
            CancellationToken cancellationToken = default
        )
        {
            var options = new InvoiceItemCreateOptions
            {
                CustomerId = customerId,
                Currency = _stripeOptions.Currency,
                Amount = amount,
                Description = transactionDescription,
                TaxRates = _stripeOptions.TaxRateIds,
                Metadata = new Dictionary<string, string>
                {
                    ["transactionId"] = transactionId.ToString()
                }
            };

            await _invoiceItemService.CreateAsync(options, cancellationToken: cancellationToken);
        }

        private async Task CreateInvoiceAsync(Guid transactionId, string customerId, CancellationToken cancellationToken = default)
        {
            var options = new InvoiceCreateOptions
            {
                CustomerId = customerId,
                AutoAdvance = true,
                Metadata = new Dictionary<string, string>
                {
                    ["transactionId"] = transactionId.ToString()
                }
            };

            await _invoiceService.CreateAsync(options, cancellationToken: cancellationToken);
        }
    }
}

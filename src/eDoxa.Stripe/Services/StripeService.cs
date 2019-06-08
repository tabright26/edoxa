// Filename: StripeService.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Models;

using Microsoft.Extensions.Configuration;

using Stripe;

namespace eDoxa.Stripe.Services
{
    public sealed class StripeService : IStripeService
    {
        private readonly IConfiguration _configuration;
        private readonly AccountService _accountService;
        private readonly CardService _cardService;
        private readonly CustomerService _customerService;
        private readonly ExternalAccountService _externalAccountService;
        private readonly InvoiceItemService _invoiceItemService;
        private readonly InvoiceService _invoiceService;
        private readonly TransferService _transferService;

        public StripeService(
            IConfiguration configuration,
            AccountService accountService,
            CardService cardService,
            CustomerService customerService,
            ExternalAccountService externalAccountService,
            InvoiceService invoiceService,
            InvoiceItemService invoiceItemService,
            TransferService transferService
        )
        {
            _configuration = configuration;
            _accountService = accountService;
            _cardService = cardService;
            _customerService = customerService;
            _customerService.ExpandDefaultSource = true;
            _externalAccountService = externalAccountService;
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
            _transferService = transferService;
        }

        public async Task<StripeConnectAccountId> CreateAccountAsync(
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
            var options = new AccountCreateOptions
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
                Country = StripeConstants.Country,
                DefaultCurrency = StripeConstants.Currency,
                BusinessType = StripeConstants.BusinessType,
                Type = StripeConstants.AccountType,
                Metadata = new Dictionary<string, string>
                {
                    ["UserId"] = userId.ToString()
                }
            };

            var account = await _accountService.CreateAsync(options, cancellationToken: cancellationToken);

            return new StripeConnectAccountId(account.Id);
        }

        public async Task VerifyAccountAsync(
            StripeConnectAccountId connectAccountId,
            string line1,
            string line2,
            string city,
            string state,
            string postalCode,
            CancellationToken cancellationToken = default
        )
        {
            var options = new AccountUpdateOptions
            {
                Individual = new PersonUpdateOptions
                {
                    Address = new AddressOptions
                    {
                        Line1 = line1,
                        Line2 = line2,
                        City = city,
                        State = state,
                        PostalCode = postalCode
                    }
                },
                TosAcceptance = new AccountTosAcceptanceOptions
                {
                    Date = DateTime.UtcNow,
                    Ip = Network.GetIpAddress().ToString()
                }
            };

            await _accountService.UpdateAsync(connectAccountId.ToString(), options, cancellationToken: cancellationToken);
        }

        public async Task<StripeBankAccountId> CreateBankAccountAsync(
            StripeConnectAccountId connectAccountId,
            string externalAccountTokenId,
            CancellationToken cancellationToken = default
        )
        {
            var options = new ExternalAccountCreateOptions
            {
                ExternalAccountTokenId = externalAccountTokenId
            };

            var bankAccount = await _externalAccountService.CreateAsync(connectAccountId.ToString(), options, cancellationToken: cancellationToken);

            return new StripeBankAccountId(bankAccount.Id);
        }

        public async Task DeleteBankAccountAsync(
            StripeConnectAccountId connectAccountId,
            StripeBankAccountId bankAccountId,
            CancellationToken cancellationToken = default
        )
        {
            await _externalAccountService.DeleteAsync(connectAccountId.ToString(), bankAccountId.ToString(), cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(StripeCustomerId customerId)
        {
            var list = await _cardService.ListAsync(customerId.ToString());

            return list.Where(card => !card.Deleted ?? true).ToList();
        }

        public async Task CreateCardAsync(StripeCustomerId customerId, string sourceToken, CancellationToken cancellationToken = default)
        {
            var options = new CardCreateOptions
            {
                SourceToken = sourceToken
            };

            await _cardService.CreateAsync(customerId.ToString(), options, cancellationToken: cancellationToken);
        }

        public async Task DeleteCardAsync(StripeCustomerId customerId, StripeCardId cardId, CancellationToken cancellationToken = default)
        {
            await _cardService.DeleteAsync(customerId.ToString(), cardId.ToString(), cancellationToken: cancellationToken);
        }

        public async Task<Customer> GetCustomerAsync(StripeCustomerId customerId, CancellationToken cancellationToken = default)
        {
            return await _customerService.GetAsync(customerId.ToString(), cancellationToken: cancellationToken);
        }

        public async Task<StripeCustomerId> CreateCustomerAsync(
            Guid userId,
            StripeConnectAccountId connectAccountId,
            string email,
            CancellationToken cancellationToken = default
        )
        {
            var options = new CustomerCreateOptions
            {
                Email = email,
                Metadata = new Dictionary<string, string>
                {
                    ["userId"] = userId.ToString(),
                    [nameof(StripeConnectAccountId)] = connectAccountId.ToString()
                }
            };

            var customer = await _customerService.CreateAsync(options, cancellationToken: cancellationToken);

            return new StripeCustomerId(customer.Id);
        }

        public async Task UpdateCardDefaultAsync(StripeCustomerId customerId, StripeCardId cardId, CancellationToken cancellationToken = default)
        {
            var options = new CustomerUpdateOptions
            {
                DefaultSource = cardId.ToString()
            };

            await _customerService.UpdateAsync(customerId.ToString(), options, cancellationToken: cancellationToken);
        }

        public async Task CreateInvoiceAsync(
            StripeCustomerId customerId,
            long price,
            Guid transactionId,
            string transactionDescription,
            CancellationToken cancellationToken = default
        )
        {
            await this.CreateInvoiceItemAsync(
                customerId,
                price,
                transactionId,
                transactionDescription,
                cancellationToken
            );

            await this.CreateInvoiceAsync(customerId, transactionId, cancellationToken);
        }

        public async Task CreateTransferAsync(
            StripeConnectAccountId connectAccountId,
            long price,
            Guid transactionId,
            string transactionDescription,
            CancellationToken cancellationToken = default
        )
        {
            var options = new TransferCreateOptions
            {
                Currency = StripeConstants.Currency,
                Amount = price,
                Destination = connectAccountId.ToString(),
                Description = transactionDescription,
                Metadata = new Dictionary<string, string>
                {
                    ["transactionId"] = transactionId.ToString()
                }
            };

            await _transferService.CreateAsync(options, cancellationToken: cancellationToken);
        }

        private async Task CreateInvoiceItemAsync(
            StripeCustomerId customerId,
            long price,
            Guid transactionId,
            string transactionDescription,
            CancellationToken cancellationToken = default
        )
        {
            var options = new InvoiceItemCreateOptions
            {
                CustomerId = customerId.ToString(),
                Description = transactionDescription,
                Amount = price,
                Currency = StripeConstants.Currency,
                TaxRates = _configuration.GetSection("TaxRateIds").Get<List<string>>(),
                Metadata = new Dictionary<string, string>
                {
                    ["transactionId"] = transactionId.ToString()
                }
            };

            await _invoiceItemService.CreateAsync(options, cancellationToken: cancellationToken);
        }

        private async Task CreateInvoiceAsync(StripeCustomerId customerId, Guid transactionId, CancellationToken cancellationToken = default)
        {
            var options = new InvoiceCreateOptions
            {
                CustomerId = customerId.ToString(),
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

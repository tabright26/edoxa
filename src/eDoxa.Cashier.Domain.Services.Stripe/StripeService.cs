// Filename: StripeService.cs
// Date Created: 2019-05-13
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

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;
using eDoxa.Security;

using Microsoft.Extensions.Configuration;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe
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
            TransferService transferService)
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

        public async Task<StripeAccountId> CreateAccountAsync(UserId userId, string email, string firstName, string lastName, int year, int month, int day, CancellationToken cancellationToken = default)
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
                    [nameof(UserId)] = userId.ToString()
                }
            };

            var account = await _accountService.CreateAsync(options, cancellationToken: cancellationToken);

            return new StripeAccountId(account.Id);
        }

        public async Task VerifyAccountAsync(StripeAccountId accountId, string line1, string line2, string city, string state, string postalCode, CancellationToken cancellationToken = default)
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
                    Ip = Host.GetIpAddress().ToString()
                }
            };

            await _accountService.UpdateAsync(accountId.ToString(), options, cancellationToken: cancellationToken);
        }

        public async Task<StripeBankAccountId> CreateBankAccountAsync(StripeAccountId accountId, string externalAccountTokenId, CancellationToken cancellationToken = default)
        {
            var options = new ExternalAccountCreateOptions
            {
                ExternalAccountTokenId = externalAccountTokenId
            };

            var bankAccount = await _externalAccountService.CreateAsync(accountId.ToString(), options, cancellationToken: cancellationToken);

            return new StripeBankAccountId(bankAccount.Id);
        }

        public async Task DeleteBankAccountAsync(StripeAccountId accountId, StripeBankAccountId bankAccountId, CancellationToken cancellationToken = default)
        {
            await _externalAccountService.DeleteAsync(accountId.ToString(), bankAccountId.ToString(), cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(StripeCustomerId customerId)
        {
            var list = await _cardService.ListAsync(customerId.ToString());

            return list.Where(card => !card.Deleted ?? true).ToList();
        }

        public async Task<Option<Card>> GetCardAsync(StripeCustomerId customerId, StripeCardId cardId)
        {
            var card = await _cardService.GetAsync(customerId.ToString(), cardId.ToString());

            return card != null ? new Option<Card>(card) : new Option<Card>();
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

        public async Task<StripeCustomerId> CreateCustomerAsync(StripeAccountId accountId, UserId userId, string email, CancellationToken cancellationToken = default)
        {
            var options = new CustomerCreateOptions
            {
                Email = email,
                Metadata = new Dictionary<string, string>
                {
                    [nameof(UserId)] = userId.ToString(),
                    [nameof(StripeAccountId)] = accountId.ToString()
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

        public async Task CreateInvoiceAsync(StripeCustomerId customerId, IBundle bundle, ITransaction transaction, CancellationToken cancellationToken = default)
        {
            await this.CreateInvoiceItemAsync(customerId, bundle, transaction, cancellationToken);

            await this.CreateInvoiceAsync(customerId, transaction, cancellationToken);
        }

        public async Task CreateTransfer(StripeAccountId accountId, IBundle bundle, ITransaction transaction, CancellationToken cancellationToken = default)
        {
            var options = new TransferCreateOptions
            {
                Currency = StripeConstants.Currency,
                Amount = bundle.Price.AsCents(),
                Destination = accountId.ToString(),
                Description = transaction.Description.ToString(),
                Metadata = new Dictionary<string, string>
                {
                    [nameof(TransactionId)] = transaction.Id.ToString()
                }
            };

            await _transferService.CreateAsync(options, cancellationToken: cancellationToken);
        }

        private async Task CreateInvoiceItemAsync(StripeCustomerId customerId, IBundle bundle, ITransaction transaction, CancellationToken cancellationToken = default)
        {
            var options = new InvoiceItemCreateOptions
            {
                CustomerId = customerId.ToString(),
                Description = transaction.Description.ToString(),
                Amount = bundle.Price.AsCents(),
                Currency = StripeConstants.Currency,
                TaxRates = _configuration.GetSection("TaxRateIds").Get<List<string>>(),
                Metadata = new Dictionary<string, string>
                {
                    [nameof(TransactionId)] = transaction.Id.ToString()
                }
            };

            await _invoiceItemService.CreateAsync(options, cancellationToken: cancellationToken);
        }

        private async Task CreateInvoiceAsync(StripeCustomerId customerId, ITransaction transaction, CancellationToken cancellationToken = default)
        {
            var options = new InvoiceCreateOptions
            {
                CustomerId = customerId.ToString(),
                AutoAdvance = true,
                Metadata = new Dictionary<string, string>
                {
                    [nameof(TransactionId)] = transaction.Id.ToString()
                }
            };

            await _invoiceService.CreateAsync(options, cancellationToken: cancellationToken);
        }
    }
}
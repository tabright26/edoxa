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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Extensions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Domain.Services.Stripe.Validations;
using eDoxa.Functional;
using eDoxa.Security;

using Microsoft.Extensions.Configuration;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe
{
    public sealed class StripeService : IStripeService
    {
        private static readonly StripeValidator StripeValidator = new StripeValidator();
        private readonly AccountService _accountService;
        private readonly CardService _cardService;
        private readonly IConfiguration _configuration;
        private readonly CustomerService _customerService;
        private readonly ExternalAccountService _externalAccountService;
        private readonly InvoiceItemService _invoiceItemService;
        private readonly InvoiceService _invoiceService;
        private readonly TransferService _transferService;

        public StripeService(
            AccountService accountService,
            CardService cardService,
            CustomerService customerService,
            ExternalAccountService externalAccountService,
            InvoiceService invoiceService,
            InvoiceItemService invoiceItemService,
            TransferService transferService,
            IConfiguration configuration)
        {
            _accountService = accountService;
            _cardService = cardService;
            _customerService = customerService;
            _externalAccountService = externalAccountService;
            _customerService.ExpandDefaultSource = true;
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
            _transferService = transferService;
            _configuration = configuration;
        }

        public async Task<StripeAccountId> CreateAccountAsync(
            UserId userId,
            string email,
            string firstName,
            string lastName,
            int year,
            int month,
            int day,
            CancellationToken cancellationToken = default)
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
                ExternalAccountTokenId = externalAccountTokenId // TestToken = "btok_us_verified"
            };

            var bankAccount = await _externalAccountService.CreateAsync(accountId.ToString(), options, cancellationToken: cancellationToken);

            return new StripeBankAccountId(bankAccount.Id);
        }

        public async Task DeleteBankAccountAsync(StripeAccountId accountId, StripeBankAccountId bankAccountId, CancellationToken cancellationToken = default)
        {
            await _externalAccountService.DeleteAsync(accountId.ToString(), bankAccountId.ToString(), cancellationToken: cancellationToken);
        }

        public async Task<Option<StripeList<Card>>> ListCardsAsync(StripeCustomerId customerId)
        {
            var list = await _cardService.ListAsync(customerId.ToString());

            return list.Any() ? new Option<StripeList<Card>>(list.Where(card => !card.Deleted ?? true).ToStripeList()) : new Option<StripeList<Card>>();
        }

        public async Task<Option<Card>> GetCardAsync(StripeCustomerId customerId, StripeCardId cardId)
        {
            var card = await _cardService.GetAsync(customerId.ToString(), cardId.ToString());

            return card != null ? new Option<Card>(card) : new Option<Card>();
        }

        public async Task<Either<ValidationResult, Card>> CreateCardAsync(
            StripeCustomerId customerId,
            string sourceToken,
            bool defaultSource,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var card = await _cardService.CreateAsync(
                    customerId.ToString(),
                    new CardCreateOptions
                    {
                        SourceToken = sourceToken
                    },
                    cancellationToken: cancellationToken
                );

                if (defaultSource)
                {
                    await this.UpdateDefaultSourceAsync(customerId, new StripeCardId(card.Id), cancellationToken);
                }

                return card;
            }
            catch (StripeException exception)
            {
                return new ValidationResult(exception.Message);
            }
        }

        public async Task<Either<ValidationResult, Card>> DeleteCardAsync(StripeCustomerId customerId, StripeCardId cardId, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _cardService.DeleteAsync(customerId.ToString(), cardId.ToString(), cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                return new ValidationResult(exception.Message);
            }
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

        public async Task<Either<ValidationResult, Customer>> UpdateCustomerDefaultSourceAsync(
            StripeCustomerId customerId,
            StripeCardId cardId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.UpdateDefaultSourceAsync(customerId, cardId, cancellationToken);
            }
            catch (StripeException exception)
            {
                return new ValidationResult(exception.Message);
            }
        }

        public async Task<Either<ValidationResult, Invoice>> CreateInvoiceAsync(
            StripeCustomerId customerId,
            string email,
            IBundle bundle,
            ITransaction transaction,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var customer = await _customerService.GetAsync(customerId.ToString(), cancellationToken: cancellationToken);

                if (!StripeValidator.Validate(customer, out var result))
                {
                    return result;
                }

                await this.CreateInvoiceItemAsync(customerId, bundle, transaction, cancellationToken);

                return await _invoiceService.CreateAsync(new InvoiceCreateOptions
                {
                    CustomerId = customer.Id,
                    AutoAdvance = true,
                    DefaultSource = customer.DefaultSource.Id,
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(TransactionId)] = transaction.Id.ToString()
                    }
                }, cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                return new ValidationResult(exception.Message);
            }
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

        private async Task<Customer> UpdateDefaultSourceAsync(
            StripeCustomerId customerId,
            StripeCardId cardId,
            CancellationToken cancellationToken = default)
        {
            return await _customerService.UpdateAsync(customerId.ToString(), new CustomerUpdateOptions
            {
                DefaultSource = cardId.ToString()
            }, cancellationToken: cancellationToken);
        }

        private async Task CreateInvoiceItemAsync(
            StripeCustomerId customerId,
            IBundle bundle,
            ITransaction transaction,
            CancellationToken cancellationToken = default)
        {
            await _invoiceItemService.CreateAsync(new InvoiceItemCreateOptions
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
            }, cancellationToken: cancellationToken);
        }
    }
}
// Filename: StripeService.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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

using Microsoft.Extensions.Configuration;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe
{
    public sealed class StripeService : IStripeService
    {
        private static readonly StripeValidator StripeValidator = new StripeValidator();
        private readonly BankAccountService _bankAccountService;
        private readonly CardService _cardService;
        private readonly IConfiguration _configuration;
        private readonly CustomerService _customerService;
        private readonly InvoiceItemService _invoiceItemService;
        private readonly InvoiceService _invoiceService;

        public StripeService(
            BankAccountService bankAccountService,
            CardService cardService,
            CustomerService customerService,
            InvoiceService invoiceService,
            InvoiceItemService invoiceItemService,
            IConfiguration configuration)
        {
            _bankAccountService = bankAccountService;
            _cardService = cardService;
            _customerService = customerService;
            _customerService.ExpandDefaultSource = true;
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
            _configuration = configuration;
        }

        public async Task<Option<BankAccount>> GetUserBankAccountAsync(CustomerId customerId)
        {
            var bankAccounts = await _bankAccountService.ListAsync(customerId.ToString(), new BankAccountListOptions());

            var bankAccount = bankAccounts.FirstOrDefault(x => !x.Deleted ?? true);

            return bankAccount != null ? new Option<BankAccount>(bankAccount) : new Option<BankAccount>();
        }

        public async Task<Either<ValidationResult, BankAccount>> CreateBankAccountAsync(
            CustomerId customerId,
            string sourceToken,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var bankAccount = await this.GetUserBankAccountAsync(customerId);

                if (bankAccount.SingleOrDefault() != null)
                {
                    return new ValidationResult("A bank account is already associated with this customer.");
                }

                return await _bankAccountService.CreateAsync(customerId.ToString(), new BankAccountCreateOptions
                {
                    SourceToken = sourceToken
                }, cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                return new ValidationResult(exception.Message);
            }
        }

        public async Task<Either<ValidationResult, BankAccount>> DeleteBankAccountAsync(CustomerId customerId, CancellationToken cancellationToken = default)
        {
            try
            {
                var bankAccount = (await this.GetUserBankAccountAsync(customerId)).SingleOrDefault();

                if (bankAccount == null)
                {
                    return new ValidationResult("No bank account is associated with this customer.");
                }

                return await _bankAccountService.DeleteAsync(customerId.ToString(), bankAccount.Id, cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                return new ValidationResult(exception.Message);
            }
        }

        public async Task<Option<StripeList<Card>>> ListCardsAsync(CustomerId customerId)
        {
            var list = await _cardService.ListAsync(customerId.ToString());

            return list.Any() ? new Option<StripeList<Card>>(list.Where(card => !card.Deleted ?? true).ToStripeList()) : new Option<StripeList<Card>>();
        }

        public async Task<Option<Card>> GetCardAsync(CustomerId customerId, CardId cardId)
        {
            var card = await _cardService.GetAsync(customerId.ToString(), cardId.ToString());

            return card != null ? new Option<Card>(card) : new Option<Card>();
        }

        public async Task<Either<ValidationResult, Card>> CreateCardAsync(
            CustomerId customerId,
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
                    await this.UpdateDefaultSourceAsync(customerId, new CardId(card.Id), cancellationToken);
                }

                return card;
            }
            catch (StripeException exception)
            {
                return new ValidationResult(exception.Message);
            }
        }

        public async Task<Either<ValidationResult, Card>> DeleteCardAsync(CustomerId customerId, CardId cardId, CancellationToken cancellationToken = default)
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

        public async Task<Either<ValidationResult, Customer>> CreateCustomerAsync(UserId userId, string email, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _customerService.CreateAsync(new CustomerCreateOptions
                {
                    Email = email,
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(UserId)] = userId.ToString()
                    }
                }, cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                return new ValidationResult(exception.Message);
            }
        }

        public async Task<Either<ValidationResult, Customer>> UpdateCustomerDefaultSourceAsync(
            CustomerId customerId,
            CardId cardId,
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
            CustomerId customerId,
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

        private async Task<Customer> UpdateDefaultSourceAsync(
            CustomerId customerId,
            CardId cardId,
            CancellationToken cancellationToken = default)
        {
            return await _customerService.UpdateAsync(customerId.ToString(), new CustomerUpdateOptions
            {
                DefaultSource = cardId.ToString()
            }, cancellationToken: cancellationToken);
        }

        private async Task CreateInvoiceItemAsync(
            CustomerId customerId,
            IBundle bundle,
            ITransaction transaction,
            CancellationToken cancellationToken = default)
        {
            await _invoiceItemService.CreateAsync(new InvoiceItemCreateOptions
            {
                CustomerId = customerId.ToString(),
                Description = transaction.Description.ToString(),
                Amount = bundle.Price.AsCents(),
                Currency = "usd",
                TaxRates = _configuration.GetSection("TaxRateIds").Get<List<string>>(),
                Metadata = new Dictionary<string, string>
                {
                    [nameof(TransactionId)] = transaction.Id.ToString()
                }
            }, cancellationToken: cancellationToken);
        }
    }
}
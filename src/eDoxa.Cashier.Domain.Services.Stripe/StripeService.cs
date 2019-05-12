// Filename: StripeService.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Functional;

using FluentValidation.Results;

using Stripe;

namespace eDoxa.Cashier.Domain.Services.Stripe
{
    public sealed class StripeService : IStripeService
    {
        private readonly BankAccountService _bankAccountService;
        private readonly CardService _cardService;
        private readonly CustomerService _customerService;
        private readonly InvoiceItemService _invoiceItemService;
        private readonly InvoiceService _invoiceService;
        private readonly PayoutService _payoutService;

        public StripeService(
            BankAccountService bankAccountService,
            CardService cardService,
            CustomerService customerService,
            InvoiceService invoiceService,
            InvoiceItemService invoiceItemService,
            PayoutService payoutService)
        {
            _bankAccountService = bankAccountService;
            _cardService = cardService;
            _customerService = customerService;
            _customerService.ExpandDefaultSource = true;
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
            _payoutService = payoutService;
        }

        public async Task<Either<ValidationResult, BankAccount>> CreateBankAccountAsync(
            CustomerId customerId,
            string sourceToken,
            CancellationToken cancellationToken = default)
        {
            var failures = new List<ValidationFailure>();

            try
            {
                return await _bankAccountService.CreateAsync(customerId.ToString(), new BankAccountCreateOptions
                {
                    SourceToken = sourceToken
                }, cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                failures.Add(new ValidationFailure(string.Empty, exception.Message));

                return new ValidationResult(failures);
            }
        }

        public async Task<Either<ValidationResult, BankAccount>> DeleteBankAccountAsync(CustomerId customerId, CancellationToken cancellationToken = default)
        {
            var failures = new List<ValidationFailure>();

            try
            {
                var bankAccounts = await _bankAccountService.ListAsync(customerId.ToString(), cancellationToken: cancellationToken);

                var bankAccount = bankAccounts.FirstOrDefault();

                if (bankAccount == null)
                {
                    failures.Add(new ValidationFailure(string.Empty, "Account already deleted."));

                    return new ValidationResult(failures);
                }

                return await _bankAccountService.DeleteAsync(customerId.ToString(), bankAccount.Id, cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                failures.Add(new ValidationFailure(string.Empty, exception.Message));

                return new ValidationResult(failures);
            }
        }

        public async Task<Either<ValidationResult, Card>> CreateCardAsync(
            CustomerId customerId,
            string sourceToken,
            bool defaultSource,
            CancellationToken cancellationToken = default)
        {
            var failures = new List<ValidationFailure>();

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
                    await _customerService.UpdateAsync(
                        customerId.ToString(),
                        new CustomerUpdateOptions
                        {
                            DefaultSource = card.Id
                        },
                        cancellationToken: cancellationToken
                    );
                }

                return card;
            }
            catch (StripeException exception)
            {
                failures.Add(new ValidationFailure(string.Empty, exception.Message));

                return new ValidationResult(failures);
            }
        }

        public async Task<Either<ValidationResult, Card>> DeleteCardAsync(CustomerId customerId, CardId cardId, CancellationToken cancellationToken = default)
        {
            var failures = new List<ValidationFailure>();

            try
            {
                return await _cardService.DeleteAsync(customerId.ToString(), cardId.ToString(), cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                failures.Add(new ValidationFailure(string.Empty, exception.Message));

                return new ValidationResult(failures);
            }
        }

        public async Task<Either<ValidationResult, Customer>> CreateCustomerAsync(UserId userId, string email, CancellationToken cancellationToken = default)
        {
            var failures = new List<ValidationFailure>();

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
                failures.Add(new ValidationFailure(string.Empty, exception.Message));

                return new ValidationResult(failures);
            }
        }

        public async Task<Either<ValidationResult, Customer>> UpdateCustomerEmailAsync(
            CustomerId customerId,
            string email,
            CancellationToken cancellationToken = default)
        {
            var failures = new List<ValidationFailure>();

            try
            {
                return await _customerService.UpdateAsync(customerId.ToString(), new CustomerUpdateOptions
                {
                    Email = email
                }, cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                failures.Add(new ValidationFailure(string.Empty, exception.Message));

                return new ValidationResult(failures);
            }
        }

        public async Task<Either<ValidationResult, Customer>> UpdateCustomerDefaultSourceAsync(
            CustomerId customerId,
            CardId cardId,
            CancellationToken cancellationToken = default)
        {
            var failures = new List<ValidationFailure>();

            try
            {
                return await _customerService.UpdateAsync(customerId.ToString(), new CustomerUpdateOptions
                {
                    DefaultSource = cardId.ToString()
                }, cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                failures.Add(new ValidationFailure(string.Empty, exception.Message));

                return new ValidationResult(failures);
            }
        }

        public async Task<Either<ValidationResult, Invoice>> CreateInvoiceAsync(
            CustomerId customerId,
            IBundle bundle,
            ITransaction transaction,
            CancellationToken cancellationToken = default)
        {
            var failures = new List<ValidationFailure>();

            try
            {
                var customer = await _customerService.GetAsync(customerId.ToString(), cancellationToken: cancellationToken);

                if (customer.DefaultSource == null)
                {
                    failures.Add(new ValidationFailure(string.Empty,
                        "The customer default source payment is invalid. This customer doesn't have any default payment source."));

                    return new ValidationResult(failures);
                }

                if (customer.DefaultSource.Object != "card")
                {
                    failures.Add(new ValidationFailure(string.Empty, "The customer default source payment is invalid. Only credit card are accepted."));

                    return new ValidationResult(failures);
                }

                await _invoiceItemService.CreateAsync(new InvoiceItemCreateOptions
                {
                    CustomerId = customerId.ToString(),
                    Description = transaction.Description.ToString(),
                    Amount = bundle.Price.AsCents(),
                    Currency = "usd",
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(TransactionId)] = transaction.Id.ToString()
                    }
                }, cancellationToken: cancellationToken);

                return await _invoiceService.CreateAsync(new InvoiceCreateOptions
                {
                    CustomerId = customer.Id,
                    TaxPercent = 15M,
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
                failures.Add(new ValidationFailure(string.Empty, exception.Message));

                return new ValidationResult(failures);
            }
        }

        public async Task<Either<ValidationResult, Payout>> CreatePayoutAsync(
            CustomerId customerId,
            IBundle bundle,
            ITransaction transaction,
            CancellationToken cancellationToken = default)
        {
            var failures = new List<ValidationFailure>();

            try
            {
                var customer = await _customerService.GetAsync(customerId.ToString(), cancellationToken: cancellationToken);

                if (customer.DefaultSource == null)
                {
                    failures.Add(new ValidationFailure(string.Empty,
                        "The customer default source payment is invalid. This customer doesn't have any default payment source."));

                    return new ValidationResult(failures);
                }

                if (customer.DefaultSource?.Object != "card")
                {
                    failures.Add(new ValidationFailure(string.Empty, "The customer default source payment is invalid. Only credit card are accepted."));

                    return new ValidationResult(failures);
                }

                return await _payoutService.CreateAsync(new PayoutCreateOptions
                {
                    Amount = bundle.Price.AsCents(),
                    Destination = customer.DefaultSourceId,
                    Currency = "usd",
                    StatementDescriptor = "eDoxa",
                    SourceType = "card",
                    Method = "standard",
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(TransactionId)] = transaction.Id.ToString()
                    }
                }, cancellationToken: cancellationToken);
            }
            catch (StripeException exception)
            {
                failures.Add(new ValidationFailure(string.Empty, exception.Message));

                return new ValidationResult(failures);
            }
        }
    }
}
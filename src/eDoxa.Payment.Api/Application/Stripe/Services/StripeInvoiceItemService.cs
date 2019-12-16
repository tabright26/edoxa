// Filename: StripeInvoiceItemService.cs
// Date Created: 2019-12-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Services
{
    public sealed class StripeInvoiceItemService : InvoiceItemService, IStripeInvoiceItemService
    {
        public StripeInvoiceItemService(IOptions<StripeOptions> options)
        {
            Options = options.Value.Invoice;
        }

        private StripeInvoiceOptions Options { get; }

        public async Task CreateInvoiceItemAsync(
            TransactionId transactionId,
            string description,
            string customerId,
            long amount
        )
        {
            await this.CreateAsync(
                new InvoiceItemCreateOptions
                {
                    Customer = customerId,
                    Currency = Options.Currency,
                    Amount = amount,
                    Description = description,
                    TaxRates = Options.TaxRates,
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(transactionId)] = transactionId.ToString()
                    }
                });
        }
    }
}

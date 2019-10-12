// Filename: StripeInvoiceItemService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Models;
using eDoxa.Payment.Domain.Stripe.Services;

using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripeInvoiceItemService : InvoiceItemService, IStripeInvoiceItemService
    {
        public StripeInvoiceItemService(IOptions<StripeOptions> options)
        {
            Options = options.Value;
        }

        private StripeOptions Options { get; }

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
                    CustomerId = customerId,
                    Currency = Options.Currency,
                    Amount = amount,
                    Description = description,
                    TaxRates = Options.TaxRateIds,
                    Metadata = new Dictionary<string, string>
                    {
                        ["transactionId"] = transactionId.ToString()
                    }
                });
        }
    }
}

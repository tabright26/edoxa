// Filename: StripeInvoiceItemService.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Stripe.Services.Abstractions;

using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Stripe.Services
{
    public sealed class StripeInvoiceItemService : InvoiceItemService, IStripeInvoiceItemService
    {
        private readonly IOptions<StripeOptions> _options;

        public StripeInvoiceItemService(IOptionsSnapshot<StripeOptions> options)
        {
            _options = options;
        }

        private StripeOptions Options => _options.Value;

        public async Task CreateInvoiceItemAsync(
            string transactionId,
            string description,
            string customer,
            long amount
        )
        {
            var options = new InvoiceItemCreateOptions
            {
                Customer = customer,
                Currency = Options.Invoice.Currency,
                Amount = amount,
                Description = description,
                TaxRates = Options.Invoice.TaxRates.ToList(),
                Metadata = new Dictionary<string, string>
                {
                    [nameof(transactionId)] = transactionId
                }
            };

            await this.CreateAsync(options);
        }
    }
}

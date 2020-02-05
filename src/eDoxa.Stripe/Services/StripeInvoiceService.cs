// Filename: StripeInvoiceService.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Stripe.Services.Abstractions;

using Stripe;

namespace eDoxa.Stripe.Services
{
    public sealed class StripeInvoiceService : InvoiceService, IStripeInvoiceService
    {
        private readonly IStripeInvoiceItemService _stripeInvoiceItemService;

        public StripeInvoiceService(IStripeInvoiceItemService stripeInvoiceItemService)
        {
            _stripeInvoiceItemService = stripeInvoiceItemService;
        }

        public async Task<Invoice> CreateInvoiceAsync(
            string customer,
            string transactionId,
            long amount,
            string description
        )
        {
            await _stripeInvoiceItemService.CreateInvoiceItemAsync(
                transactionId,
                description,
                customer,
                amount);

            return await this.CreateInvoiceAsync(transactionId, customer);
        }

        private async Task<Invoice> CreateInvoiceAsync(string transactionId, string customer)
        {
            return await this.CreateAsync(
                new InvoiceCreateOptions
                {
                    Customer = customer,
                    AutoAdvance = true,
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(transactionId)] = transactionId
                    }
                });
        }
    }
}

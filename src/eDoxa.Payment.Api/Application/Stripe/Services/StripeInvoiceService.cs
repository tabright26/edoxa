// Filename: StripeInvoiceService.cs
// Date Created: 2019-12-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Payment.Api.Application.Stripe.Services.Abstractions;
using eDoxa.Seedwork.Domain.Misc;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Services
{
    public sealed class StripeInvoiceService : InvoiceService, IStripeInvoiceService
    {
        private readonly IStripeInvoiceItemService _stripeInvoiceItemService;

        public StripeInvoiceService(IStripeInvoiceItemService stripeInvoiceItemService)
        {
            _stripeInvoiceItemService = stripeInvoiceItemService;
        }

        public async Task<Invoice> CreateInvoiceAsync(
            string customerId,
            TransactionId transactionId,
            long amount,
            string description
        )
        {
            await _stripeInvoiceItemService.CreateInvoiceItemAsync(
                transactionId,
                description,
                customerId,
                amount);

            return await this.CreateInvoiceAsync(transactionId, customerId);
        }

        private async Task<Invoice> CreateInvoiceAsync(TransactionId transactionId, string customerId)
        {
            return await this.CreateAsync(
                new InvoiceCreateOptions
                {
                    Customer = customerId,
                    AutoAdvance = true,
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(transactionId)] = transactionId.ToString()
                    }
                });
        }
    }
}

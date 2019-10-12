// Filename: StripeInvoiceService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Models;
using eDoxa.Payment.Domain.Stripe.Services;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripeInvoiceService : InvoiceService, IStripeInvoiceService
    {
        private readonly IStripeInvoiceItemService _stripeInvoiceItemService;

        public StripeInvoiceService(IStripeInvoiceItemService stripeInvoiceItemService)
        {
            _stripeInvoiceItemService = stripeInvoiceItemService;
        }

        public async Task CreateInvoiceAsync(
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

            await this.CreateInvoiceAsync(transactionId, customerId);
        }

        private async Task CreateInvoiceAsync(TransactionId transactionId, string customerId)
        {
            await this.CreateAsync(
                new InvoiceCreateOptions
                {
                    CustomerId = customerId,
                    AutoAdvance = true,
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(transactionId)] = transactionId.ToString()
                    }
                });
        }
    }
}

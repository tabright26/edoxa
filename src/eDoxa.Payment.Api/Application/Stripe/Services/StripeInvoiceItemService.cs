// Filename: StripeInvoiceItemService.cs
// Date Created: 2019-12-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Payment.Options;
using eDoxa.Payment.Api.Application.Stripe.Services.Abstractions;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Services
{
    public sealed class StripeInvoiceItemService : InvoiceItemService, IStripeInvoiceItemService
    {
        private readonly IOptions<PaymentApiOptions> _optionsSnapshot;

        public StripeInvoiceItemService(IOptionsSnapshot<PaymentApiOptions> optionsSnapshot)
        {
            _optionsSnapshot = optionsSnapshot;
        }

        private PaymentApiOptions Options => _optionsSnapshot.Value;

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
                    Currency = Options.Default.Stripe.Invoice.Currency,
                    Amount = amount,
                    Description = description,
                    TaxRates = Options.Default.Stripe.Invoice.TaxRates.ToList(),
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(transactionId)] = transactionId.ToString()
                    }
                });
        }
    }
}

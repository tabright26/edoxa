// Filename: IStripeInvoiceService.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Stripe;

namespace eDoxa.Stripe.Services.Abstractions
{
    public interface IStripeInvoiceService
    {
        Task<Invoice> CreateInvoiceAsync(
            string customer,
            string transactionId,
            long amount,
            string description
        );
    }
}

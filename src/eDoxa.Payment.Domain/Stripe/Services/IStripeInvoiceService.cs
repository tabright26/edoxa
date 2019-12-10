// Filename: IStripeInvoiceService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Misc;

using Stripe;

namespace eDoxa.Payment.Domain.Stripe.Services
{
    public interface IStripeInvoiceService
    {
        Task<Invoice> CreateInvoiceAsync(
            string customerId,
            TransactionId transactionId,
            long amount,
            string description
        );
    }
}

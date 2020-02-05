// Filename: IStripeInvoiceService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Misc;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Services.Abstractions
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

// Filename: IStripeInvoiceItemService.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Stripe.Services.Abstractions
{
    public interface IStripeInvoiceItemService
    {
        Task CreateInvoiceItemAsync(
            string transactionId,
            string description,
            string customer,
            long amount
        );
    }
}

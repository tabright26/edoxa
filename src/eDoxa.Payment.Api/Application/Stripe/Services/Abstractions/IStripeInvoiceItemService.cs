// Filename: IStripeInvoiceItemService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Payment.Api.Application.Stripe.Services.Abstractions
{
    public interface IStripeInvoiceItemService
    {
        Task CreateInvoiceItemAsync(
            TransactionId transactionId,
            string description,
            string customerId,
            long amount
        );
    }
}

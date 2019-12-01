// Filename: IStripeTransferService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Payment.Domain.Stripe.Services
{
    public interface IStripeTransferService
    {
        Task CreateTransferAsync(
            string accountId,
            TransactionId transactionId,
            long amount,
            string description
        );
    }
}

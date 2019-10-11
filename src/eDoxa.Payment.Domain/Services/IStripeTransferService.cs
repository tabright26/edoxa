// Filename: IStripeTransferService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;

namespace eDoxa.Payment.Domain.Services
{
    public interface IStripeTransferService
    {
        Task CreateTransferAsync(
            TransactionId transactionId,
            string description,
            string accountId,
            long amount
        );
    }
}

// Filename: IPaypalPayoutService.cs
// Date Created: 2020-02-04
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using PayPal.Api;

namespace eDoxa.Paypal.Services.Abstractions
{
    public interface IPaypalPayoutService
    {
        Task<PayoutBatch> CreateAsync(
            string transactionId,
            string email,
            int amount,
            string? description = null,
            string? correlationId = null
        );
    }
}

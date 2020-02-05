// Filename: IPaypalService.cs
// Date Created: 2020-02-04
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

namespace eDoxa.Paypal.Services.Abstractions
{
    public interface IPaypalService
    {
        Task WithdrawAsync(
            string transactionId,
            string email,
            int amount,
            string correlationId = null
        );
    }
}

// Filename: IStripeExternalAccountService.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Stripe;

namespace eDoxa.Payment.Domain.Services
{
    public interface IStripeExternalAccountService
    {
        Task<IExternalAccount?> FindBankAccountAsync(string accountId);

        Task<IExternalAccount> UpdateBankAccountAsync(string accountId, string token);
    }
}

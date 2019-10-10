// Filename: IStripeExternalAccountService.cs
// Date Created: 2019-10-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Stripe;

namespace eDoxa.Payment.Domain.Services
{
    public interface IStripeExternalAccountService
    {
        Task<StripeList<IExternalAccount>> FetchBankAccountsAsync(string accountId);

        Task<IExternalAccount> ChangeBankAccountAsync(string accountId, string token);
    }
}

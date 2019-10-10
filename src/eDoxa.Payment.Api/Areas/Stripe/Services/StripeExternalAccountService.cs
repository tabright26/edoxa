// Filename: StripeExternalAccountService.cs
// Date Created: 2019-10-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Services;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripeExternalAccountService : ExternalAccountService, IStripeExternalAccountService
    {
        public async Task<StripeList<IExternalAccount>> FetchBankAccountsAsync(string accountId)
        {
            return await this.ListAsync(
                accountId,
                new ExternalAccountListOptions
                {
                    Limit = 1,
                    ExtraParams =
                    {
                        ["object"] = "bank_account"
                    }
                });
        }

        public async Task<IExternalAccount> ChangeBankAccountAsync(string accountId, string token)
        {
            var externalAccounts = await this.FetchBankAccountsAsync(accountId);

            var externalAccount = await this.CreateAsync(
                accountId,
                new ExternalAccountCreateOptions
                {
                    ExternalAccount = token,
                    DefaultForCurrency = true
                });

            if (externalAccounts.Any())
            {
                await this.DeleteBankAccountAsync(accountId, externalAccounts.First().Id);
            }

            return externalAccount;
        }

        private async Task DeleteBankAccountAsync(string accountId, string bankAccountId)
        {
            await this.DeleteAsync(accountId, bankAccountId);
        }
    }
}

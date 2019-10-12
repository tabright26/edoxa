// Filename: IStripeAccountService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;

using Stripe;

namespace eDoxa.Payment.Domain.Services
{
    public interface IStripeAccountService
    {
        Task<string> GetAccountIdAsync(UserId userId);

        Task<Account> GetAccountAsync(string accountId);

        Task<string> CreateAccountAsync(
            UserId userId,
            string email,
            string country,
            string customerId
        );

        Task UpdateIndividualAsync(string accountId, PersonUpdateOptions individual);
    }
}

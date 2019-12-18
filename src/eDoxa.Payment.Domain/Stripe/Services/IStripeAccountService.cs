// Filename: IStripeAccountService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Stripe;

namespace eDoxa.Payment.Domain.Stripe.Services
{
    public interface IStripeAccountService
    {
        Task<string> GetAccountIdAsync(UserId userId);

        Task<Account> GetAccountAsync(string accountId);

        Task<string> CreateAccountAsync(
            UserId userId,
            string email,
            Country country,
            string customerId
        );

        Task<IDomainValidationResult> UpdateIndividualAsync(string accountId, PersonUpdateOptions individual);

        Task<bool> HasAccountVerifiedAsync(string accountId);
    }
}

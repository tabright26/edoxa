// Filename: StripeAccountService.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;
using eDoxa.Payment.Domain.Repositories;
using eDoxa.Payment.Domain.Services;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripeAccountService : AccountService, IStripeAccountService
    {
        private readonly IStripeRepository _stripeRepository;

        public StripeAccountService(IStripeRepository stripeRepository)
        {
            _stripeRepository = stripeRepository;
        }

        public async Task<string> GetAccountIdAsync(UserId userId)
        {
            var reference = await _stripeRepository.GetReferenceAsync(userId);

            return reference.ConnectAccountId;
        }

        public async Task<string?> FindAccountIdAsync(UserId userId)
        {
            var reference = await _stripeRepository.FindReferenceAsync(userId);

            return reference?.ConnectAccountId;
        }

        public async Task<string> CreateAccountAsync(UserId userId, string email, string country)
        {
            var account = await this.CreateAsync(
                new AccountCreateOptions
                {
                    Type = "custom",
                    BusinessType = "individual",
                    Country = country,
                    Email = email,
                    Metadata = new Dictionary<string, string>
                    {
                        ["userId"] = userId.ToString()
                    }
                });

            return account.Id;
        }

        public async Task<bool> AccountIsVerifiedAsync(string accountId)
        {
            var account = await this.GetAsync(accountId);

            return !account.Requirements.CurrentlyDue.Any();
        }
    }
}

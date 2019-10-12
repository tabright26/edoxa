// Filename: StripeAccountService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Models;
using eDoxa.Payment.Domain.Stripe.Repositories;
using eDoxa.Payment.Domain.Stripe.Services;

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

            return reference.AccountId;
        }

        public async Task<Account> GetAccountAsync(string accountId)
        {
            return await this.GetAsync(accountId);
        }

        public async Task UpdateIndividualAsync(string accountId, PersonUpdateOptions individual)
        {
            await this.UpdateAsync(
                accountId,
                new AccountUpdateOptions
                {
                    Individual = individual
                });
        }

        public async Task<string> CreateAccountAsync(
            UserId userId,
            string email,
            string country,
            string customerId
        )
        {
            var account = await this.CreateAsync(
                new AccountCreateOptions
                {
                    Type = "custom",
                    BusinessType = "individual",
                    Country = country,
                    Individual = new PersonCreateOptions
                    {
                        Email = email,
                        Metadata = new Dictionary<string, string>
                        {
                            [nameof(userId)] = userId.ToString()
                        }
                    },
                    TosAcceptance = new AccountTosAcceptanceOptions // FRANCIS: Must be provided by login form.
                    {
                        Date = DateTime.UtcNow,
                        Ip = "10.10.10.10"
                    },
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(customerId)] = customerId
                    }
                });

            return account.Id;
        }
    }
}

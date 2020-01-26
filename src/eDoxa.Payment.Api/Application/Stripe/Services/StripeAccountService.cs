// Filename: StripeAccountService.cs
// Date Created: 2019-12-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Repositories;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Services
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

        public async Task<IDomainValidationResult> UpdateIndividualAsync(string accountId, PersonUpdateOptions individual)
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                try
                {
                    var account = await this.UpdateAsync(
                        accountId,
                        new AccountUpdateOptions
                        {
                            Individual = individual
                        });

                    result.AddEntityToMetadata(account);
                }
                catch (StripeException exception)
                {
                    result.AddFailedPreconditionError(exception.Message);
                }
            }

            return result;
        }

        public async Task<bool> HasAccountVerifiedAsync(string accountId)
        {
            var account = await this.GetAccountAsync(accountId);

            return account != null && !account.Requirements.CurrentlyDue.Any();
        }

        public async Task<string> CreateAccountAsync(
            UserId userId,
            string email,
            Country country,
            string ip,
            string customerId
        )
        {
            var account = await this.CreateAsync(
                new AccountCreateOptions
                {
                    Type = "custom",
                    BusinessType = "individual",
                    Country = country.Name,
                    Individual = new PersonCreateOptions
                    {
                        Email = email,
                        Metadata = new Dictionary<string, string>
                        {
                            [nameof(userId)] = userId
                        }
                    },
                    TosAcceptance = new AccountTosAcceptanceOptions
                    {
                        Date = DateTime.UtcNow,
                        Ip = ip
                    },
                    RequestedCapabilities = new List<string>
                    {
                        "card_payments",
                        "transfers"
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

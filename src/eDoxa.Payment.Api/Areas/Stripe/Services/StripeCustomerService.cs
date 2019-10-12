// Filename: StripeCustomerService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;
using eDoxa.Payment.Domain.Repositories;
using eDoxa.Payment.Domain.Services;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripeCustomerService : CustomerService, IStripeCustomerService
    {
        private readonly IStripeRepository _stripeRepository;

        public StripeCustomerService(IStripeRepository stripeRepository)
        {
            _stripeRepository = stripeRepository;
        }

        public async Task<string> GetCustomerIdAsync(UserId userId)
        {
            var reference = await _stripeRepository.GetReferenceAsync(userId);

            return reference.CustomerId;
        }

        public async Task<string> CreateCustomerAsync(UserId userId, string email)
        {
            var customer = await this.CreateAsync(
                new CustomerCreateOptions
                {
                    Email = email,
                    Metadata = new Dictionary<string, string>
                    {
                        ["userId"] = userId.ToString()
                    }
                });

            return customer.Id;
        }
    }
}

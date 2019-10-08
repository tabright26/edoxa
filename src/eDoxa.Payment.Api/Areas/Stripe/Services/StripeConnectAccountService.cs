// Filename: ConnectAccountService.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;
using eDoxa.Payment.Domain.Repositories;
using eDoxa.Payment.Domain.Services;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripeConnectAccountService : IStripeConnectAccountService
    {
        private readonly IStripeRepository _stripeRepository;

        public StripeConnectAccountService(IStripeRepository stripeRepository)
        {
            _stripeRepository = stripeRepository;
        }

        public async Task<string> GetConnectAccountIdAsync(UserId userId)
        {
            var reference = await _stripeRepository.GetReferenceAsync(userId);

            return reference.ConnectAccountId;
        }

        public async Task<string?> FindConnectAccountIdAsync(UserId userId)
        {
            var reference = await _stripeRepository.FindReferenceAsync(userId);

            return reference?.ConnectAccountId;
        }
    }
}

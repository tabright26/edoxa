﻿// Filename: StripeService.cs
// Date Created: 2019-10-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;
using eDoxa.Payment.Domain.Repositories;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public class StripeService : IStripeService
    {
        private readonly IStripeRepository _stripeRepository;

        public StripeService(IStripeRepository stripeRepository)
        {
            _stripeRepository = stripeRepository;
        }

        public async Task CreateReferenceAsync(UserId userId, string customerId, string connectAccountId)
        {
            _stripeRepository.Create(new StripeReference(userId, customerId, connectAccountId));

            await _stripeRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> ReferenceExistsAsync(UserId userId)
        {
            return await _stripeRepository.ReferenceExistsAsync(userId);
        }
    }
}

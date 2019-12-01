// Filename: StripeReferenceService.cs
// Date Created: 2019-10-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.AggregateModels.StripeAggregate;
using eDoxa.Payment.Domain.Stripe.Repositories;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripeReferenceService : IStripeReferenceService
    {
        private readonly IStripeRepository _stripeRepository;

        public StripeReferenceService(IStripeRepository stripeRepository)
        {
            _stripeRepository = stripeRepository;
        }

        public async Task CreateReferenceAsync(UserId userId, string customerId, string accountId)
        {
            _stripeRepository.Create(new StripeReference(userId, customerId, accountId));

            await _stripeRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> ReferenceExistsAsync(UserId userId)
        {
            return await _stripeRepository.ReferenceExistsAsync(userId);
        }
    }
}

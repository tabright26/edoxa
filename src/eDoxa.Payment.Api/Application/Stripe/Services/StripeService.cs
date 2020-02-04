// Filename: StripeReferenceService.cs
// Date Created: 2019-12-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.AggregateModels.StripeAggregate;
using eDoxa.Payment.Domain.Stripe.Repositories;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Payment.Api.Application.Stripe.Services
{
    public sealed class StripeService : IStripeService
    {
        private readonly IStripeRepository _stripeRepository;

        public StripeService(IStripeRepository stripeRepository)
        {
            _stripeRepository = stripeRepository;
        }

        public async Task<DomainValidationResult<StripeReference>> CreateAsync(UserId userId, string customerId, string accountId)
        {
            var result = new DomainValidationResult<StripeReference>();

            if (result.IsValid)
            {
                var reference = new StripeReference(userId, customerId, accountId);

                _stripeRepository.Create(reference);

                await _stripeRepository.UnitOfWork.CommitAsync();

                return reference;
            }

            return result;
        }

        public async Task<bool> UserExistsAsync(UserId userId)
        {
            return await _stripeRepository.ReferenceExistsAsync(userId);
        }
    }
}

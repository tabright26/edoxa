// Filename: IStripeService.cs
// Date Created: 2019-10-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.AggregateModels.StripeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Payment.Domain.Stripe.Services
{
    public interface IStripeService
    {
        Task<DomainValidationResult<StripeReference>> CreateAsync(UserId userId, string customerId, string accountId);

        Task<bool> UserExistsAsync(UserId userId);
    }
}

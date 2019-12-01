// Filename: IStripeService.cs
// Date Created: 2019-10-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Payment.Domain.Stripe.Services
{
    public interface IStripeReferenceService
    {
        Task CreateReferenceAsync(UserId userId, string customerId, string accountId);

        Task<bool> ReferenceExistsAsync(UserId userId);
    }
}

// Filename: IStripeService.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Models;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public interface IStripeService
    {
        Task CreateReferenceAsync(UserId userId, string customerId, string connectAccountId);

        Task<bool> ReferenceExistsAsync(UserId userId);
    }
}

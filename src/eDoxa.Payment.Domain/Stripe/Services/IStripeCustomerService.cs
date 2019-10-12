// Filename: ICustomerService.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Models;

namespace eDoxa.Payment.Domain.Stripe.Services
{
    public interface IStripeCustomerService
    {
        Task<string> GetCustomerIdAsync(UserId userId);

        Task<string> CreateCustomerAsync(UserId userId, string email);
    }
}

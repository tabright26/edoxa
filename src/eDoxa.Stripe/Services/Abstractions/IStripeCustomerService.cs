// Filename: IStripeCustomerService.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Stripe;

namespace eDoxa.Stripe.Services.Abstractions
{
    public interface IStripeCustomerService
    {
        Task<string> CreateCustomerAsync(string userId, string email);

        Task<bool> HasDefaultPaymentMethodAsync(string customerId);

        Task<Customer> SetDefaultPaymentMethodAsync(string customerId, string paymentMethodId);

        Task<Customer> FindCustomerAsync(string customerId);
    }
}

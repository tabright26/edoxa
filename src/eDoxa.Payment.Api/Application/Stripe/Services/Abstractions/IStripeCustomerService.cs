// Filename: ICustomerService.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Misc;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Services.Abstractions
{
    public interface IStripeCustomerService
    {
        Task<string> CreateCustomerAsync(UserId userId, string email);

        Task<bool> HasDefaultPaymentMethodAsync(string customerId);

        Task<Customer> SetDefaultPaymentMethodAsync(string customerId, string paymentMethodId);

        Task<Customer> FindCustomerAsync(string customerId);
    }
}

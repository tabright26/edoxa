// Filename: IStripePaymentMethodService.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using Stripe;

namespace eDoxa.Payment.Domain.Stripe.Services
{
    public interface IStripePaymentMethodService
    {
        Task<StripeList<PaymentMethod>> FetchPaymentMethodsAsync(string customerId, string type);

        Task<PaymentMethod> UpdatePaymentMethodAsync(string paymentMethodId, long expMonth, long expYear);

        Task<PaymentMethod> AttachPaymentMethodAsync(string paymentMethodId, string customerId);

        Task<PaymentMethod> DetachPaymentMethodAsync(string paymentMethodId);
    }
}

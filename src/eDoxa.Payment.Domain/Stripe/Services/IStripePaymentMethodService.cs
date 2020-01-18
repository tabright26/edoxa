// Filename: IStripePaymentMethodService.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;

using Stripe;

namespace eDoxa.Payment.Domain.Stripe.Services
{
    public interface IStripePaymentMethodService
    {
        Task<StripeList<PaymentMethod>> FetchPaymentMethodsAsync(string customerId);

        Task<PaymentMethod> UpdatePaymentMethodAsync(string paymentMethodId, long expMonth, long expYear);

        Task<IDomainValidationResult> AttachPaymentMethodAsync(string paymentMethodId, string customerId, bool defaultPaymentMethod = false);

        Task<PaymentMethod> DetachPaymentMethodAsync(string paymentMethodId);
    }
}

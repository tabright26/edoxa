// Filename: IStripePaymentMethodService.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;

using Stripe;

namespace eDoxa.Stripe.Services.Abstractions
{
    public interface IStripePaymentMethodService
    {
        Task<StripeList<PaymentMethod>> FetchPaymentMethodsAsync(string customer);

        Task<PaymentMethod> UpdatePaymentMethodAsync(string paymentMethodId, long expMonth, long expYear);

        Task<DomainValidationResult<PaymentMethod>> AttachPaymentMethodAsync(string paymentMethodId, string customerId, bool defaultPaymentMethod = false);

        Task<PaymentMethod> DetachPaymentMethodAsync(string paymentMethodId);
    }
}

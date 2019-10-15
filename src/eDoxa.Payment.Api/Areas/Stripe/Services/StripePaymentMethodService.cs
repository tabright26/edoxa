// Filename: StripePaymentMethodService.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Services;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Services
{
    public sealed class StripePaymentMethodService : PaymentMethodService, IStripePaymentMethodService
    {
        public async Task<StripeList<PaymentMethod>> FetchPaymentMethodsAsync(string customerId, string type)
        {
            return await this.ListAsync(
                new PaymentMethodListOptions
                {
                    CustomerId = customerId,
                    Type = type
                });
        }

        public async Task<PaymentMethod> UpdatePaymentMethodAsync(string paymentMethodId, long expMonth, long expYear)
        {
            return await this.UpdateAsync(
                paymentMethodId,
                new PaymentMethodUpdateOptions
                {
                    Card = new PaymentMethodCardUpdateOptions
                    {
                        ExpMonth = expMonth,
                        ExpYear = expYear
                    }
                });
        }

        public async Task<PaymentMethod> AttachPaymentMethodAsync(string paymentMethodId, string customerId)
        {
            return await this.AttachAsync(
                paymentMethodId,
                new PaymentMethodAttachOptions
                {
                    CustomerId = customerId
                });
        }

        public async Task<PaymentMethod> DetachPaymentMethodAsync(string paymentMethodId)
        {
            return await this.DetachAsync(paymentMethodId, new PaymentMethodDetachOptions());
        }
    }
}

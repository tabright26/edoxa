// Filename: StripePaymentMethodService.cs
// Date Created: 2019-10-15
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
        private readonly IStripeCustomerService _stripeCustomerService;

        public StripePaymentMethodService(IStripeCustomerService stripeCustomerService)
        {
            _stripeCustomerService = stripeCustomerService;
        }

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

        public async Task<PaymentMethod> AttachPaymentMethodAsync(string paymentMethodId, string customerId, bool defaultPaymentMethod = false)
        {
            var paymentMethod = await this.AttachAsync(
                paymentMethodId,
                new PaymentMethodAttachOptions
                {
                    CustomerId = customerId
                });

            if (defaultPaymentMethod || !await _stripeCustomerService.HasDefaultPaymentMethodAsync(customerId))
            {
                await _stripeCustomerService.SetDefaultPaymentMethodAsync(customerId, paymentMethodId);
            }

            return paymentMethod;
        }

        public async Task<PaymentMethod> DetachPaymentMethodAsync(string paymentMethodId)
        {
            return await this.DetachAsync(paymentMethodId, new PaymentMethodDetachOptions());
        }
    }
}

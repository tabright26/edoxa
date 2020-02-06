// Filename: StripePaymentMethodService.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain;
using eDoxa.Stripe.Services.Abstractions;

using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Stripe.Services
{
    public sealed class StripePaymentMethodService : PaymentMethodService, IStripePaymentMethodService
    {
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IOptions<StripeOptions> _options;

        public StripePaymentMethodService(IStripeCustomerService stripeCustomerService, IOptionsSnapshot<StripeOptions> options)
        {
            _stripeCustomerService = stripeCustomerService;
            _options = options;
        }

        private StripeOptions Options => _options.Value;

        public async Task<StripeList<PaymentMethod>> FetchPaymentMethodsAsync(string customer)
        {
            var options = new PaymentMethodListOptions
            {
                Customer = customer,
                Type = "card",
                Limit = 100
            };

            return await this.ListAsync(options);
        }

        public async Task<PaymentMethod> UpdatePaymentMethodAsync(string paymentMethodId, long expMonth, long expYear)
        {
            var options = new PaymentMethodUpdateOptions
            {
                Card = new PaymentMethodCardUpdateOptions
                {
                    ExpMonth = expMonth,
                    ExpYear = expYear
                }
            };

            return await this.UpdateAsync(paymentMethodId, options);
        }

        public async Task<DomainValidationResult<PaymentMethod>> AttachPaymentMethodAsync(
            string paymentMethodId,
            string customerId,
            bool defaultPaymentMethod = false
        )
        {
            var result = new DomainValidationResult<PaymentMethod>();

            var paymentMethodCardLimit = Options.PaymentMethod.Card.Limit;

            if (await this.PaymentMethodCountAsync(customerId) >= paymentMethodCardLimit)
            {
                result.AddFailedPreconditionError(
                    $"You can have a maximum of {paymentMethodCardLimit} card{(paymentMethodCardLimit > 1 ? "s" : string.Empty)} as a payment method");
            }

            if (result.IsValid)
            {
                var options = new PaymentMethodAttachOptions
                {
                    Customer = customerId
                };

                var paymentMethod = await this.AttachAsync(paymentMethodId, options);

                if (defaultPaymentMethod || !await _stripeCustomerService.HasDefaultPaymentMethodAsync(customerId))
                {
                    await _stripeCustomerService.SetDefaultPaymentMethodAsync(customerId, paymentMethodId);
                }

                return paymentMethod;
            }

            return result;
        }

        public async Task<PaymentMethod> DetachPaymentMethodAsync(string paymentMethodId)
        {
            return await this.DetachAsync(paymentMethodId, new PaymentMethodDetachOptions());
        }

        private async Task<int> PaymentMethodCountAsync(string customerId)
        {
            var paymentMethods = await this.FetchPaymentMethodsAsync(customerId);

            return paymentMethods.Count();
        }
    }
}

// Filename: StripePaymentMethodService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Payment.Options;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain;

using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Services
{
    public sealed class StripePaymentMethodService : PaymentMethodService, IStripePaymentMethodService
    {
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IOptionsSnapshot<PaymentApiOptions> _optionsSnapshot;

        public StripePaymentMethodService(IStripeCustomerService stripeCustomerService, IOptionsSnapshot<PaymentApiOptions> optionsSnapshot)
        {
            _stripeCustomerService = stripeCustomerService;
            _optionsSnapshot = optionsSnapshot;
        }

        private PaymentApiOptions Options => _optionsSnapshot.Value;

        public async Task<StripeList<PaymentMethod>> FetchPaymentMethodsAsync(string customerId)
        {
            return await this.ListAsync(
                new PaymentMethodListOptions
                {
                    Customer = customerId,
                    Type = "card",
                    Limit = 100
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

        public async Task<IDomainValidationResult> AttachPaymentMethodAsync(string paymentMethodId, string customerId, bool defaultPaymentMethod = false)
        {
            var result = new DomainValidationResult();

            var paymentMethodLimit = Options.Static.Stripe.PaymentMethod.Card.Limit;

            if (await this.PaymentMethodCountAsync(customerId) >= Options.Static.Stripe.PaymentMethod.Card.Limit)
            {
                result.AddFailedPreconditionError(
                    $"You can have a maximum of {paymentMethodLimit} card{(paymentMethodLimit > 1 ? "s" : string.Empty)} as a payment method");
            }

            if (result.IsValid)
            {
                var paymentMethod = await this.AttachAsync(
                    paymentMethodId,
                    new PaymentMethodAttachOptions
                    {
                        Customer = customerId
                    });

                if (defaultPaymentMethod || !await _stripeCustomerService.HasDefaultPaymentMethodAsync(customerId))
                {
                    await _stripeCustomerService.SetDefaultPaymentMethodAsync(customerId, paymentMethodId);
                }

                result.AddEntityToMetadata(paymentMethod);
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

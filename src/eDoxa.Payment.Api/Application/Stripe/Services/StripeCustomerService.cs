// Filename: StripeCustomerService.cs
// Date Created: 2019-12-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Repositories;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Misc;

using Stripe;

namespace eDoxa.Payment.Api.Application.Stripe.Services
{
    public sealed class StripeCustomerService : CustomerService, IStripeCustomerService
    {
        private readonly IStripeRepository _stripeRepository;

        public StripeCustomerService(IStripeRepository stripeRepository)
        {
            _stripeRepository = stripeRepository;
        }

        public async Task<string> GetCustomerIdAsync(UserId userId)
        {
            var reference = await _stripeRepository.GetReferenceAsync(userId);

            return reference.CustomerId;
        }

        public async Task<string> CreateCustomerAsync(UserId userId, string email)
        {
            var customer = await this.CreateAsync(
                new CustomerCreateOptions
                {
                    Email = email,
                    Metadata = new Dictionary<string, string>
                    {
                        [nameof(userId)] = userId.ToString()
                    }
                });

            return customer.Id;
        }

        public async Task<bool> HasDefaultPaymentMethodAsync(string customerId)
        {
            var customer = await this.GetAsync(
                customerId,
                new CustomerGetOptions
                {
                    Expand = new List<string>
                    {
                        "invoice_settings.default_payment_method"
                    }
                });

            return customer.InvoiceSettings.DefaultPaymentMethod != null;
        }

        public async Task<Customer> SetDefaultPaymentMethodAsync(string customerId, string paymentMethodId)
        {
            return await this.UpdateAsync(
                customerId,
                new CustomerUpdateOptions
                {
                    InvoiceSettings = new CustomerInvoiceSettingsOptions
                    {
                        DefaultPaymentMethod = paymentMethodId
                    }
                });
        }

        public async Task<Customer> FindCustomerAsync(string customerId)
        {
            return await this.GetAsync(customerId);
        }
    }
}

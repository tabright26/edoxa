// Filename: StripeCustomerService.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Stripe.Services.Abstractions;

using Stripe;

namespace eDoxa.Stripe.Services
{
    public sealed class StripeCustomerService : CustomerService, IStripeCustomerService
    {
        public async Task<string> CreateCustomerAsync(string userId, string email)
        {
            var options = new CustomerCreateOptions
            {
                Email = email,
                Metadata = new Dictionary<string, string>
                {
                    [nameof(userId)] = userId
                }
            };

            var customer = await this.CreateAsync(options);

            return customer.Id;
        }

        public async Task<bool> HasDefaultPaymentMethodAsync(string customerId)
        {
            var options = new CustomerGetOptions
            {
                Expand = new List<string>
                {
                    "invoice_settings.default_payment_method"
                }
            };

            var customer = await this.GetAsync(customerId, options);

            return customer?.InvoiceSettings?.DefaultPaymentMethod != null;
        }

        public async Task<Customer> SetDefaultPaymentMethodAsync(string customerId, string paymentMethodId)
        {
            var options = new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions
                {
                    DefaultPaymentMethod = paymentMethodId
                }
            };

            return await this.UpdateAsync(customerId, options);
        }

        public async Task<Customer> FindCustomerAsync(string customerId)
        {
            return await this.GetAsync(customerId);
        }
    }
}

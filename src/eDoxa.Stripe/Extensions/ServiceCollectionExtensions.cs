// Filename: ServiceCollectionExtensions.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Stripe.Services;
using eDoxa.Stripe.Services.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stripe;

namespace eDoxa.Stripe.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddStripe(this IServiceCollection services, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:ApiKey"];
            services.Configure<StripeOptions>(configuration.GetSection("Stripe"));
            services.AddTransient<IStripeCustomerService, StripeCustomerService>();
            services.AddTransient<IStripePaymentMethodService, StripePaymentMethodService>();
            services.AddTransient<IStripeInvoiceService, StripeInvoiceService>();
            services.AddTransient<IStripeInvoiceItemService, StripeInvoiceItemService>();
        }
    }
}

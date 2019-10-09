// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Domain.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddStripe(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StripeOptions>(configuration.GetSection("Providers:Stripe"));

            services.AddTransient<AccountService>();

            services.AddTransient<CustomerService>();

            services.AddTransient<InvoiceService>();

            services.AddTransient<InvoiceItemService>();

            services.AddTransient<TransferService>();

            services.AddTransient<PaymentMethodService>();

            services.AddTransient<IStripeTempService, StripeTempService>();
        }
    }
}

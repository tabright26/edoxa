// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stripe;

namespace eDoxa.Payment.Api.Providers.Stripe.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddStripe(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StripeOptions>(configuration.GetSection("Providers:Stripe"));

            services.AddTransient<InvoiceService>();

            services.AddTransient<InvoiceItemService>();

            services.AddTransient<TransferService>();
        }
    }
}

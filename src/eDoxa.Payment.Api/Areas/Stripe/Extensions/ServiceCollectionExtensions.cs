// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Payment.Api.Areas.Stripe.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddStripe(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<StripeOptions>(configuration.GetSection("Stripe"));
        }
    }
}

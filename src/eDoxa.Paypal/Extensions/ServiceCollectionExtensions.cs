// Filename: ServiceCollectionExtensions.cs
// Date Created: 2020-02-04
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Paypal.Services;
using eDoxa.Paypal.Services.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Paypal.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPaypal(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaypalOptions>(configuration.GetSection("Paypal"));
            services.AddTransient<IPaypalPayoutService, PaypalPayoutService>();
        }
    }
}

// Filename: ServiceCollectionExtensions.cs
// Date Created: 2020-02-04
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Paypal.Services;
using eDoxa.Paypal.Services.Abstractions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using PayPal.Api;

namespace eDoxa.Paypal.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPaypal(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PaypalOptions>(configuration.GetSection("Paypal"));

            services.AddTransient<IPaypalPayoutService>(
                provider =>
                {
                    var credential = new OAuthTokenCredential(
                        configuration["Paypal:Client:Id"],
                        configuration["Paypal:Client:Secret"],
                        new Dictionary<string, string>
                        {
                            ["mode"] = configuration["Paypal:Mode"]
                        });

                    return new PaypalPayoutService(credential, provider.GetRequiredService<IOptionsSnapshot<PaypalOptions>>());
                });
        }
    }
}

// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static void UseStripe(this IApplicationBuilder _, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["StripeConfiguration:ApiKey"];
        }
    }
}

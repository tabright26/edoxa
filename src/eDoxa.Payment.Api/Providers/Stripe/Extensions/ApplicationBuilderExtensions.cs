// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

using Stripe;

namespace eDoxa.Payment.Api.Providers.Stripe.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static void UseStripe(this IApplicationBuilder _, IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["StripeConfiguration:ApiKey"];
        }
    }
}

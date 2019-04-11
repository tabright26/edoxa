// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-04-11
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

using Stripe;

namespace eDoxa.Stripe.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseStripe(this IApplicationBuilder builder, IConfiguration configuration)
        {
            StripeConfiguration.SetApiKey(configuration["StripeConfiguration:ApiKey"]);

            return builder;
        }
    }
}
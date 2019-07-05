// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Payment.Api.Providers.Stripe.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Payment.Api.Providers.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static void UseProviders(this IApplicationBuilder application, IConfiguration configuration)
        {
            application.UseStripe(configuration);
        }
    }
}

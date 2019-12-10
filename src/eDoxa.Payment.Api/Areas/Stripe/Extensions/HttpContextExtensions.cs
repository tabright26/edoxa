// Filename: HttpContextExtensions.cs
// Date Created: 2019-12-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Payment.Api.Areas.Stripe.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetStripeAccountId(this HttpContext context) // TODO: Add claim to ClaimTypes.cs.
        {
            return context.GetClaimOrDefault("stripe:connectAccountId") ?? throw new InvalidOperationException();
        }

        public static string GetStripeCustomertId(this HttpContext context) // TODO: Add claim to ClaimTypes.cs.
        {
            return context.GetClaimOrDefault("stripe:customerId") ?? throw new InvalidOperationException();
        }
    }
}

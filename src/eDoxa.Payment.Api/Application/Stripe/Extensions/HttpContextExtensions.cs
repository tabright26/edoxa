// Filename: HttpContextExtensions.cs
// Date Created: 2019-12-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Payment.Api.Application.Stripe.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetStripeCustomertId(this HttpContext context)
        {
            return context.GetClaim(CustomClaimTypes.StripeCustomer);
        }
    }
}

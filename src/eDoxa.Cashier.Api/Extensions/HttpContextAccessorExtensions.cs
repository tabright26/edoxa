// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Http;

using static eDoxa.Seedwork.Security.ClaimTypes;

using static IdentityModel.JwtClaimTypes;

namespace eDoxa.Cashier.Api.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static UserId GetUserId(this IHttpContextAccessor accessor)
        {
            return UserId.Parse(accessor.GetClaimOrDefault(Subject) ?? throw new ArgumentNullException(Subject));
        }

        public static string? GetCustomerId(this IHttpContextAccessor accessor)
        {
            return accessor.GetClaimOrDefault(StripeCustomerId) ?? throw new ArgumentNullException(StripeCustomerId);
        }

        public static string? GetConnectAccountId(this IHttpContextAccessor accessor)
        {
            return accessor.GetClaimOrDefault(StripeConnectAccountId) ?? throw new ArgumentNullException(StripeConnectAccountId);
        }

        public static string? GetCustomerId(this HttpContext httpContext)
        {
            return httpContext.GetClaimOrDefault(StripeCustomerId) ?? throw new ArgumentNullException(StripeCustomerId);
        }

        public static string? GetConnectAccountId(this HttpContext httpContext)
        {
            return httpContext.GetClaimOrDefault(StripeConnectAccountId) ?? throw new ArgumentNullException(StripeConnectAccountId);
        }

        public static UserId GetUserId(this HttpContext httpContext)
        {
            return UserId.Parse(httpContext.GetClaimOrDefault(Subject) ?? throw new ArgumentNullException(Subject));
        }
    }
}

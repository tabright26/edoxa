// Filename: HttpContextExtensions.cs
// Date Created: 2019-07-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Security.Extensions;

using IdentityModel;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserId GetUserId(this HttpContext httpContext)
        {
            return UserId.Parse(httpContext.GetClaimOrDefault(JwtClaimTypes.Subject) ?? throw new ArgumentNullException(JwtClaimTypes.Subject));
        }
    }
}

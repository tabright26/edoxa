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

using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Http;

using static IdentityModel.JwtClaimTypes;

namespace eDoxa.Organizations.Clans.Api.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static UserId GetUserId(this IHttpContextAccessor accessor)
        {
            return UserId.Parse(accessor.GetClaimOrDefault(Subject) ?? throw new ArgumentNullException(Subject));
        }

        public static UserId GetUserId(this HttpContext httpContext)
        {
            return UserId.Parse(httpContext.GetClaimOrDefault(Subject) ?? throw new ArgumentNullException(Subject));
        }

    }
}

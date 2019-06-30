// Filename: HttpContextExtensions.cs
// Date Created: 2019-06-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Security.Extensions;

using IdentityModel;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserId GetUserId(this HttpContext httpContext)
        {
            return UserId.Parse(httpContext.GetClaimOrDefault(JwtClaimTypes.Subject) ?? throw new NullReferenceException(JwtClaimTypes.Subject));
        }
    }
}

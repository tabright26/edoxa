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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Seedwork.Security.Extensions;

using Microsoft.AspNetCore.Http;

using static IdentityModel.JwtClaimTypes;

namespace eDoxa.Arena.Challenges.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserId GetUserId(this HttpContext httpContext)
        {
            return UserId.Parse(httpContext.GetClaimOrDefault(Subject) ?? throw new ArgumentNullException(Subject));
        }
    }
}

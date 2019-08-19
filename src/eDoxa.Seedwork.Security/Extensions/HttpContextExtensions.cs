﻿// Filename: HttpContextExtensions.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetClaimOrDefault(this HttpContext httpContext, string claimType)
        {
            return httpContext.User?.Claims?.SingleOrDefault(claim => claim.Type == claimType)?.Value;
        }
    }
}

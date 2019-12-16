// Filename: HttpContextExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;

using IdentityModel;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetClaimOrNull(this HttpContext httpContext, string claimType)
        {
            return httpContext.User?.Claims?.SingleOrDefault(claim => claim.Type == claimType)?.Value;
        }

        public static string GetClaim(this HttpContext httpContext, string claimType)
        {
            return httpContext.GetClaimOrNull(claimType) ?? throw new InvalidOperationException(claimType);
        }

        public static UserId GetUserId(this HttpContext httpContext)
        {
            return httpContext.GetClaim(JwtClaimTypes.Subject).ParseEntityId<UserId>();
        }

        public static string GetEmail(this HttpContext httpContext)
        {
            return httpContext.GetClaim(JwtClaimTypes.Email);
        }

        public static PlayerId? GetGamePlayerIdOrNull(this HttpContext httpContext, Game game)
        {
            return httpContext.GetClaimOrNull(CustomClaimTypes.GetGamePlayerFor(game))?.ParseStringId<PlayerId>();
        }
    }
}

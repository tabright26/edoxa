// Filename: HttpContextExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Misc;

using IdentityModel;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class HttpContextExtensions
    {
        private static string? GetClaimOrDefault(this HttpContext httpContext, string claimType)
        {
            return httpContext.User?.Claims?.SingleOrDefault(claim => claim.Type == claimType)?.Value;
        }

        public static UserId GetUserId(this HttpContext httpContext)
        {
            return UserId.Parse(httpContext.GetClaimOrDefault(JwtClaimTypes.Subject) ?? throw new ArgumentNullException(JwtClaimTypes.Subject));
        }

        public static string GetEmail(this HttpContext httpContext)
        {
            return httpContext.GetClaimOrDefault(JwtClaimTypes.Email) ?? throw new ArgumentNullException(JwtClaimTypes.Email);
        }

        public static PlayerId GetPlayerId(this HttpContext httpContext, Game game)
        {
            return PlayerId.Parse(httpContext.GetClaimOrDefault($"games/{game.NormalizedName}") ?? throw new ArgumentNullException(nameof(Game)));
        }
    }
}

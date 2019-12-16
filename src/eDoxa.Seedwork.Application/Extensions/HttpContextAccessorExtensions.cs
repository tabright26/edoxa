// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string? GetClaimOrNull(this IHttpContextAccessor accessor, string claimType)
        {
            return accessor.HttpContext.GetClaimOrNull(claimType);
        }

        public static string GetClaim(this IHttpContextAccessor accessor, string claimType)
        {
            return accessor.HttpContext.GetClaim(claimType);
        }

        public static UserId GetUserId(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext.GetUserId();
        }

        public static string GetEmail(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext.GetEmail();
        }

        public static PlayerId? GetGamePlayerIdOrNull(this IHttpContextAccessor accessor, Game game)
        {
            return accessor.HttpContext.GetGamePlayerIdOrNull(game);
        }
    }
}

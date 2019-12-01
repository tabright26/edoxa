﻿// Filename: HttpContextAccessorExtensions.cs
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
        public static UserId GetUserId(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext.GetUserId();
        }

        public static string GetEmail(this IHttpContextAccessor accessor)
        {
            return accessor.HttpContext.GetEmail();
        }

        public static PlayerId GetPlayerId(this IHttpContextAccessor accessor, Game game)
        {
            return accessor.HttpContext.GetPlayerId(game);
        }
    }
}

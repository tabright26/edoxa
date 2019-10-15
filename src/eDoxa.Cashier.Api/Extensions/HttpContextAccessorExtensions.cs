// Filename: HttpContextAccessorExtensions.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Extensions
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
    }
}

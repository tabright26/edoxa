// Filename: HttpContextExtensions.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Http;

using static IdentityModel.JwtClaimTypes;

namespace eDoxa.Payment.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static UserId GetUserId(this HttpContext httpContext)
        {
            return UserId.Parse(httpContext.GetClaimOrDefault(Subject) ?? throw new ArgumentNullException(Subject));
        }

        public static string GetEmail(this HttpContext httpContext)
        {
            return httpContext.GetClaimOrDefault(Email) ?? throw new ArgumentNullException(Email);
        }
    }
}

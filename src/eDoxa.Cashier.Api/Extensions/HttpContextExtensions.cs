// Filename: HttpContextExtensions.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Http;

using static IdentityModel.JwtClaimTypes;

namespace eDoxa.Cashier.Api.Extensions
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

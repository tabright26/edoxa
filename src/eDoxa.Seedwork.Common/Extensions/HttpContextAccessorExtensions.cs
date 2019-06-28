using System;

using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Security.Extensions;

using IdentityModel;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Common.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static UserId GetUserId(this IHttpContextAccessor accessor)
        {
            return UserId.Parse(accessor.GetClaimOrDefault(JwtClaimTypes.Subject) ?? throw new NullReferenceException(JwtClaimTypes.Subject));
        }
    }
}

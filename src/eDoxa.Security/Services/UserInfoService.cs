// Filename: UserInfoService.cs
// Date Created: 2019-05-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Functional.Maybe;

using IdentityModel;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Security.Services
{
    public sealed class UserInfoService : IUserInfoService
    {
        private readonly HttpContext _httpContext;

        public UserInfoService(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext;
        }

        public Option<Guid> Subject =>
            this.TryGetClaim(JwtClaimTypes.Subject).Select(value => new Option<Guid>(new Guid(value))).DefaultIfEmpty(new Option<Guid>()).Single();

        private Option<string> TryGetClaim(string type)
        {
            var value = _httpContext.User.Claims.SingleOrDefault(claim => claim.Type == type)?.Value;

            return value != null ? new Option<string>(value) : new Option<string>();
        }
    }
}
// Filename: CashierHttpContext.cs
// Date Created: 2019-05-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Security.Execeptions;
using eDoxa.Seedwork.Domain.Entities;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Security
{
    public sealed class CashierHttpContext : ICashierHttpContext
    {
        private readonly HttpContext _httpContext;

        public CashierHttpContext([CanBeNull] IHttpContextAccessor accessor)
        {
            _httpContext = accessor?.HttpContext ??
                           throw new ArgumentNullException(nameof(accessor), "IHttpContextAccessor was injected from an invalid HttpContext.");
        }

        public UserId UserId => UserId.Parse(this.TryGetClaim(JwtClaimTypes.Subject) ?? throw new ClaimException(JwtClaimTypes.Subject));

        [CanBeNull]
        private string TryGetClaim(string claimType)
        {
            return _httpContext.User.Claims.SingleOrDefault(claim => claim.Type == claimType)?.Value;
        }
    }
}

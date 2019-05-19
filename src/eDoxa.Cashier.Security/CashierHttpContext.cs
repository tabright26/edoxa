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

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Security;
using eDoxa.Security.Execeptions;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Security
{
    public sealed class CashierHttpContext : ICashierHttpContext
    {
        private readonly HttpContext _httpContext;

        public CashierHttpContext(IHttpContextAccessor accessor)
        {
            _httpContext = accessor?.HttpContext ??
                           throw new ArgumentNullException(nameof(accessor), "IHttpContextAccessor was injected from an invalid HttpContext.");
        }

        public UserId UserId => UserId.Parse(this.TryGetClaim(JwtClaimTypes.Subject) ?? throw new ClaimException(JwtClaimTypes.Subject));

        public StripeAccountId StripeAccountId =>
            new StripeAccountId(this.TryGetClaim(CustomClaimTypes.StripeAccountId) ?? throw new ClaimException(CustomClaimTypes.StripeAccountId));

        public StripeBankAccountId StripeBankAccountId =>
            new StripeBankAccountId(this.TryGetClaim(CustomClaimTypes.StripeBankAccountId) ?? throw new ClaimException(CustomClaimTypes.StripeBankAccountId));

        public StripeCustomerId StripeCustomerId =>
            new StripeCustomerId(this.TryGetClaim(CustomClaimTypes.StripeCustomerId) ?? throw new ClaimException(CustomClaimTypes.StripeCustomerId));

        [CanBeNull]
        private string TryGetClaim(string claimType)
        {
            var t = _httpContext.User.Claims.SingleOrDefault(claim => claim.Type == claimType)?.Value;

            return t;
        }
    }
}

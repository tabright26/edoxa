// Filename: CashierSecurity.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Security;
using eDoxa.Security.Execeptions;

using IdentityModel;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Security
{
    public sealed class CashierSecurity : ICashierSecurity
    {
        private readonly IEnumerable<Claim> _claims;

        public CashierSecurity(IHttpContextAccessor accessor)
        {
            _claims = accessor?.HttpContext?.User?.Claims ??
                      throw new ArgumentNullException(nameof(accessor), "IHttpContextAccessor was injected from an invalid HttpContext.");
        }

        public UserId UserId => UserId.Parse(this.TryGetClaim(JwtClaimTypes.Subject) ?? throw new MissingClaimException());

        public StripeAccountId StripeAccountId => new StripeAccountId(this.TryGetClaim(CustomClaimTypes.StripeAccountId) ?? throw new MissingClaimException());

        public StripeBankAccountId StripeBankAccountId =>
            new StripeBankAccountId(this.TryGetClaim(CustomClaimTypes.StripeBankAccountId) ?? throw new MissingClaimException());

        public StripeCustomerId StripeCustomerId =>
            new StripeCustomerId(this.TryGetClaim(CustomClaimTypes.StripeCustomerId) ?? throw new MissingClaimException());

        public IEnumerable<string> Roles => this.TryGetClaims(JwtClaimTypes.Role);

        public IEnumerable<string> Permissions => this.TryGetClaims(CustomClaimTypes.Permission);

        public bool HasStripeBankAccount()
        {
            return this.TryGetClaim(CustomClaimTypes.StripeBankAccountId) != null;
        }

        private string TryGetClaim(string claimType)
        {
            return _claims.SingleOrDefault(claim => claim.Type == claimType)?.Value;
        }

        private IEnumerable<string> TryGetClaims(string claimType)
        {
            return _claims.Where(claim => claim.Type == claimType).Select(claim => claim.Value).ToList();
        }
    }
}
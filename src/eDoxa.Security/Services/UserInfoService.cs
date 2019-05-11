// Filename: UserInfoService.cs
// Date Created: 2019-05-05
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

using eDoxa.Security.Abstractions;

using IdentityModel;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Security.Services
{
    public sealed class UserInfoService : IUserInfoService
    {
        private readonly IEnumerable<Claim> _claims;
        private string _bankAccountId;

        public UserInfoService(IHttpContextAccessor accessor)
        {
            _claims = accessor?.HttpContext?.User?.Claims ??
                      throw new ArgumentNullException(nameof(accessor), "IHttpContextAccessor was injected from an invalid HttpContext.");
        }

        public string Subject => this.TryGetClaim(JwtClaimTypes.Subject);

        public string PreferredUserName => this.TryGetClaim(JwtClaimTypes.PreferredUserName);

        public string GivenName => this.TryGetClaim(JwtClaimTypes.GivenName);

        public string FamilyName => this.TryGetClaim(JwtClaimTypes.FamilyName);

        public string Name => this.TryGetClaim(JwtClaimTypes.Name);

        public string BirthDate => this.TryGetClaim(JwtClaimTypes.BirthDate);

        public string Email => this.TryGetClaim(JwtClaimTypes.Email);

        public string EmailVerified => this.TryGetClaim(JwtClaimTypes.EmailVerified);

        public string PhoneNumber => this.TryGetClaim(JwtClaimTypes.PhoneNumber);

        public string PhoneNumberVerified => this.TryGetClaim(JwtClaimTypes.PhoneNumberVerified);

        public string Address => this.TryGetClaim(JwtClaimTypes.Address);

        public string BankAccountId => this.TryGetClaim(CustomClaimTypes.BankAccountId);

        public string CustomerId => this.TryGetClaim(CustomClaimTypes.CustomerId);

        public IEnumerable<string> Roles => this.TryGetClaims(JwtClaimTypes.Role);

        public IEnumerable<string> Permissions => this.TryGetClaims(CustomClaimTypes.Permission);

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
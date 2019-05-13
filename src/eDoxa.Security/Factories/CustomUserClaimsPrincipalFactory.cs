// Filename: CustomUserClaimsPrincipalFactory.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.Security.Factories
{
    public sealed class CustomUserClaimsPrincipalFactory<TUser, TRole> : UserClaimsPrincipalFactory<TUser, TRole>
    where TUser : class
    where TRole : class
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<TUser> userManager,
            RoleManager<TRole> roleManager,
            IOptions<IdentityOptions> options) : base(
            userManager,
            roleManager,
            options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            //var claims = await UserManager.GetClaimsAsync(user);

            //var givenName = claims.SingleOrDefault(claim => claim.Type == JwtClaimTypes.GivenName)?.Value;

            //var familyName = claims.SingleOrDefault(claim => claim.Type == JwtClaimTypes.FamilyName)?.Value;

            //if (givenName != null && familyName != null)
            //{
            //    identity.AddClaim(new Claim(JwtClaimTypes.Name, $"{givenName} {familyName}"));
            //}

            return identity;
        }
    }
}
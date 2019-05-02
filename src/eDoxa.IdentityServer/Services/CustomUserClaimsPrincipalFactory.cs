// Filename: CustomUserClaimsPrincipalFactory.cs
// Date Created: 2019-05-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.IdentityServer.Models;

using IdentityModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.IdentityServer.Services
{
    public sealed class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> options) : base(
            userManager,
            roleManager,
            options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var claims = await UserManager.GetClaimsAsync(user);

            var givenName = claims.SingleOrDefault(claim => claim.Type == JwtClaimTypes.GivenName)?.Value;

            var familyName = claims.SingleOrDefault(claim => claim.Type == JwtClaimTypes.FamilyName)?.Value;

            if (givenName != null && familyName != null)
            {
                identity.AddClaim(new Claim(JwtClaimTypes.Name, $"{givenName} {familyName}"));
            }

            return identity;
        }
    }
}
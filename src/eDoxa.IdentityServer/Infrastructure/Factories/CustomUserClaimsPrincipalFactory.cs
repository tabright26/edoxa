// Filename: CustomUserClaimsPrincipalFactory.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.IdentityServer.Infrastructure.Factories
{
    public sealed class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public CustomUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<Role> roleManager, IOptions<IdentityOptions> options) : base(
            userManager,
            roleManager,
            options
        )
        {
        }

        [ItemNotNull]
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync([NotNull] User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var claims = await UserManager.GetClaimsAsync(user);

            var name = new PersonalName(claims);

            identity.AddClaim(new Claim(JwtClaimTypes.Name, name.ToString()));

            return identity;
        }
    }
}

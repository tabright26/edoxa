// Filename: UserClaimsPrincipalFactory.cs
// Date Created: 2019-04-01
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using IdentityModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Areas.Identity
{
    public sealed class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public UserClaimsPrincipalFactory(UserService userService, RoleService roleService, IOptions<IdentityOptions> options) : base(
            userService,
            roleService,
            options
        )
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim(JwtClaimTypes.NickName, user.Tag.ToString()));

            identity.AddClaim(new Claim(JwtClaimTypes.Name, user.Name.ToString()));

            identity.AddClaim(new Claim(JwtClaimTypes.GivenName, user.Name.FirstName));

            identity.AddClaim(new Claim(JwtClaimTypes.FamilyName, user.Name.LastName));

            identity.AddClaim(new Claim(JwtClaimTypes.BirthDate, user.BirthDate.ToString()));

            return identity;
        }
    }
}
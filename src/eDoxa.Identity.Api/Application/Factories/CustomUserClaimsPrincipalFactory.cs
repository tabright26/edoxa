// Filename: CustomUserClaimsPrincipalFactory.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Factories.Extensions;
using eDoxa.Identity.Api.Application.Managers;
using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Factories
{
    public sealed class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public CustomUserClaimsPrincipalFactory(CustomUserManager userManager, CustomRoleManager roleManager, IOptions<IdentityOptions> options) : base(
            userManager,
            roleManager,
            options
        )
        {
            UserManager = userManager;
        }

        private new CustomUserManager UserManager { get; }

        [ItemNotNull]
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync([NotNull] User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            identity.IncludeCustomClaims(user);

            identity.IncludeGameProviderClaims(await UserManager.GetGameProvidersAsync(user));

            return identity;
        }
    }
}

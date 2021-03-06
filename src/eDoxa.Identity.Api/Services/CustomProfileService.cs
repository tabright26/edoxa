﻿// Filename: CustomProfileService.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.Services;

using IdentityServer4.Models;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Services
{
    internal sealed class CustomProfileService : IProfileService
    {
        private readonly IOptions<IdentityOptions> _optionsSnapshot;

        public CustomProfileService(CustomUserClaimsPrincipalFactory principalFactory, IUserService userService, IOptionsSnapshot<IdentityOptions> optionsSnapshot)
        {
            _optionsSnapshot = optionsSnapshot;
            PrincipalFactory = principalFactory;
            UserService = userService;
        }

        private CustomUserClaimsPrincipalFactory PrincipalFactory { get; }

        private IUserService UserService { get; }

        private IdentityOptions Options => _optionsSnapshot.Value;

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await UserService.GetUserAsync(context.Subject);

            var principal = await PrincipalFactory.CreateAsync(user);

            foreach (var claim in principal.Claims)
            {
                context.IssuedClaims.Add(claim);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await UserService.GetUserAsync(context.Subject);

            if (user != null)
            {
                context.IsActive = !await UserService.IsLockedOutAsync(user);

                if (UserService.SupportsUserSecurityStamp)
                {
                    var claims = await UserService.GetClaimsAsync(user);

                    var securityStamp = claims.SingleOrDefault(claim => claim.Type == Options.ClaimsIdentity.SecurityStampClaimType)?.Value;

                    context.IsActive = securityStamp != await UserService.GetSecurityStampAsync(user);
                }
            }
            else
            {
                context.IsActive = false;
            }
        }
    }
}

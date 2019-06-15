// Filename: CustomProfileService.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Security.Constants;

using IdentityServer4.Models;
using IdentityServer4.Services;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.IdentityServer.Infrastructure.Services
{
    internal sealed class CustomProfileService<TUser> : IProfileService
    where TUser : IdentityUser<Guid>
    {
        private readonly IUserClaimsPrincipalFactory<TUser> _userClaimsPrincipalFactory;
        private readonly UserManager<TUser> _userManager;

        public CustomProfileService(IUserClaimsPrincipalFactory<TUser> userClaimsPrincipalFactory, UserManager<TUser> userManager)
        {
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync([NotNull] ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            principal.Claims.ForEach(claim => context.IssuedClaims.Add(claim));
        }

        public async Task IsActiveAsync([NotNull] IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            if (user != null)
            {
                context.IsActive = !await _userManager.IsLockedOutAsync(user);

                if (_userManager.SupportsUserSecurityStamp)
                {
                    var claims = await _userManager.GetClaimsAsync(user);

                    var securityStamp = claims.SingleOrDefault(claim => claim.Type == CustomClaimTypes.SecurityStamp)?.Value;

                    context.IsActive = securityStamp != await _userManager.GetSecurityStampAsync(user);
                }
            }
            else
            {
                context.IsActive = false;
            }
        }
    }
}
// Filename: ProfileService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using IdentityServer4.Models;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Areas.Identity
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _factory;
        private readonly UserService _userService;
        private readonly IdentityOptions _options;

        public ProfileService(
            IUserClaimsPrincipalFactory<User> factory,
            UserService userService,
            IOptions<IdentityOptions> optionsAccessor,
            ILoggerFactory loggerFactory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _options = optionsAccessor.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));
            Logger = loggerFactory.CreateLogger<ProfileService>();
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(Logger);

            var user = await _userService.GetUserAsync(context.Subject) ?? throw new NullReferenceException(nameof(context.Subject));

            var principal = await _factory.CreateAsync(user);

            foreach (var claim in principal.Claims)
            {
                context.IssuedClaims.Add(claim);
            }

            context.LogIssuedClaims(Logger);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = false;

            var user = await _userService.GetUserAsync(context.Subject);

            if (user != null)
            {
                if (_userService.SupportsUserSecurityStamp)
                {
                    var claims = await _userService.GetClaimsAsync(user);

                    var securityStamp = claims.SingleOrDefault(claim => claim.Type == _options.ClaimsIdentity.SecurityStampClaimType)?.Value;

                    if (securityStamp != null)
                    {
                        if (await _userService.GetSecurityStampAsync(user) != securityStamp)
                        {
                            return;
                        }
                    }
                }

                context.IsActive = !await _userService.IsLockedOutAsync(user);
            }
        }

        protected ILogger Logger { get; }
    }
}
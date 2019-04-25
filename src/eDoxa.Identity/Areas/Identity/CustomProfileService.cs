// Filename: ProfileService.cs
// Date Created: 2019-04-14
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

using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using IdentityServer4.Models;
using IdentityServer4.Services;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Areas.Identity
{
    public class CustomProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _factory;
        private readonly ILogger<CustomProfileService> _logger;
        private readonly IdentityOptions _options;
        private readonly UserService _userService;

        public CustomProfileService(
            IUserClaimsPrincipalFactory<User> factory,
            UserService userService,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<CustomProfileService> logger)
        {
            _factory = factory;
            _userService = userService;
            _options = optionsAccessor.Value;
            _logger = logger;
        }

        public async Task GetProfileDataAsync([NotNull] ProfileDataRequestContext context)
        {
            context.LogProfileRequest(_logger);

            var user = await _userService.GetUserAsync(context.Subject) ?? throw new NullReferenceException(nameof(context.Subject));

            var principal = await _factory.CreateAsync(user);

            foreach (var claim in principal.Claims)
            {
                context.IssuedClaims.Add(claim);
            }

            context.LogIssuedClaims(_logger);
        }

        public async Task IsActiveAsync([NotNull] IsActiveContext context)
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
    }
}
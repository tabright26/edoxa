// Filename: SignInService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Application.Services
{
    public class SignInService : SignInManager<User>
    {
        public SignInService(
            UserService userService,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes) : base(userService, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
            UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        private UserService UserService { get; }

        [ItemNotNull]
        public override async Task<SignInResult> PasswordSignInAsync([NotNull] User user, [NotNull] string password, bool isPersistent, bool lockoutOnFailure)
        {
            var result = await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);

            if (result == SignInResult.Success)
            {
                await UserService.ConnectAsync(user);
            }

            return result;
        }

        public async Task SignOutAsync(User user)
        {
            await base.SignOutAsync();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await UserService.DisconnectAsync(user);
        }
    }
}
﻿// Filename: CustomSignInManager.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Services
{
    public sealed class SignInService : SignInManager<User>, ISignInService
    {
        public SignInService(
            UserService userService,
            IHttpContextAccessor contextAccessor,
            CustomUserClaimsPrincipalFactory claimsFactory,
            IOptionsSnapshot<IdentityOptions> optionsAccessor,
            ILogger<SignInService> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation
        ) : base(
            userService,
            contextAccessor,
            claimsFactory,
            optionsAccessor,
            logger,
            schemes,
            confirmation
        )
        {
        }
    }
}

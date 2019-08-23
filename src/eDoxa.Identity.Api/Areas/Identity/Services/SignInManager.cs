// Filename: CustomSignInManager.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Infrastructure.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class SignInManager : SignInManager<User>, ISignInManager
    {
        public SignInManager(
            UserManager userManager,
            IHttpContextAccessor contextAccessor,
            CustomUserClaimsPrincipalFactory claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager> logger,
            IAuthenticationSchemeProvider schemes
        ) : base(
            userManager,
            contextAccessor,
            claimsFactory,
            optionsAccessor,
            logger,
            schemes
        )
        {
        }
    }
}

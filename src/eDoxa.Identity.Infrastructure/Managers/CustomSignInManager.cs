// Filename: CustomSignInManager.cs
// Date Created: 2019-07-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Infrastructure.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Infrastructure.Managers
{
    public sealed class CustomSignInManager : SignInManager<UserModel>
    {
        public CustomSignInManager(
            UserManager<UserModel> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<UserModel> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<CustomSignInManager> logger,
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

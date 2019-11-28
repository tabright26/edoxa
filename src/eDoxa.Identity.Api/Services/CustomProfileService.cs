// Filename: CustomProfileService.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using IdentityServer4.Models;
using IdentityServer4.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Services
{
    internal sealed class CustomProfileService : IProfileService
    {
        public CustomProfileService(CustomUserClaimsPrincipalFactory principalFactory, IUserManager userManager, IOptions<IdentityOptions> optionsAccessor)
        {
            PrincipalFactory = principalFactory;
            UserManager = userManager;
            Options = optionsAccessor.Value;
        }

        private CustomUserClaimsPrincipalFactory PrincipalFactory { get; }

        private IUserManager UserManager { get; }

        private IdentityOptions Options { get; }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await UserManager.GetUserAsync(context.Subject);

            var principal = await PrincipalFactory.CreateAsync(user);

            foreach (var claim in principal.Claims)
            {
                context.IssuedClaims.Add(claim);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await UserManager.GetUserAsync(context.Subject);

            if (user != null)
            {
                context.IsActive = !await UserManager.IsLockedOutAsync(user);

                if (UserManager.SupportsUserSecurityStamp)
                {
                    var claims = await UserManager.GetClaimsAsync(user);

                    var securityStamp = claims.SingleOrDefault(claim => claim.Type == Options.ClaimsIdentity.SecurityStampClaimType)?.Value;

                    context.IsActive = securityStamp != await UserManager.GetSecurityStampAsync(user);
                }
            }
            else
            {
                context.IsActive = false;
            }
        }
    }
}

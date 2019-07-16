// Filename: CustomUserClaimsPrincipalFactory.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Infrastructure.Models;

using IdentityModel;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eDoxa.IdentityServer.Infrastructure.Factories
{
    public sealed class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserModel, RoleModel>
    {
        public CustomUserClaimsPrincipalFactory(
            UserManager<UserModel> userManager,
            RoleManager<RoleModel> roleManager,
            IOptions<IdentityOptions> options
        ) : base(userManager, roleManager, options)
        {
        }

        [ItemNotNull]
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync([NotNull] UserModel userModel)
        {
            var identity = await base.GenerateClaimsAsync(userModel);

            identity.AddClaim(new Claim(JwtClaimTypes.BirthDate, userModel.BirthDate.ToString("yyyy-MM-dd")));

            identity.AddClaim(new Claim(JwtClaimTypes.GivenName, userModel.FirstName));

            identity.AddClaim(new Claim(JwtClaimTypes.FamilyName, userModel.LastName));

            identity.AddClaim(new Claim(JwtClaimTypes.Name, $"{userModel.FirstName} {userModel.LastName}"));

            return identity;
        }
    }
}

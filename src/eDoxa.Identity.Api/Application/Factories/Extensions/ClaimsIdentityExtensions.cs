// Filename: ClaimsIdentityExtensions.cs
// Date Created: 2019-07-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using eDoxa.Identity.Api.Models;

using IdentityModel;

using IdentityServer4;

using JetBrains.Annotations;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Application.Factories.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static void IncludeCustomClaims(this ClaimsIdentity identity, [NotNull] UserModel user)
        {
            identity.AddClaim(new Claim(JwtClaimTypes.BirthDate, user.BirthDate.ToString("yyyy-MM-dd")));

            identity.AddClaim(new Claim(JwtClaimTypes.GivenName, user.FirstName));

            identity.AddClaim(new Claim(JwtClaimTypes.FamilyName, user.LastName));

            identity.AddClaim(new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
        }

        public static void IncludeGameProviderClaims(this ClaimsIdentity identity, IEnumerable<UserGameProviderInfo> gameProviders)
        {
            identity.AddClaim(
                new Claim("games", JsonConvert.SerializeObject(gameProviders.ToDictionary(x => x.GameProvider.Name, x => x.ProviderKey), Formatting.Indented), IdentityServerConstants.ClaimValueTypes.Json)
            );
        }
    }
}

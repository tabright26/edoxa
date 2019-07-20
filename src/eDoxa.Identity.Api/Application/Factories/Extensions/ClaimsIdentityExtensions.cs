// Filename: ClaimsIdentityExtensions.cs
// Date Created: 2019-07-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Newtonsoft.Json;

using static eDoxa.Seedwork.Security.Constants.CustomClaimTypes;

using static IdentityModel.JwtClaimTypes;

using static IdentityServer4.IdentityServerConstants.ClaimValueTypes;

using static Newtonsoft.Json.JsonConvert;

namespace eDoxa.Identity.Api.Application.Factories.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static void IncludeCustomClaims(this ClaimsIdentity identity, [NotNull] User user)
        {
            identity.AddClaim(new Claim(BirthDate, user.BirthDate.ToString("yyyy-MM-dd")));

            identity.AddClaim(new Claim(GivenName, user.FirstName));

            identity.AddClaim(new Claim(FamilyName, user.LastName));

            identity.AddClaim(new Claim(Name, $"{user.FirstName} {user.LastName}"));
        }

        public static void IncludeGameProviderClaims(this ClaimsIdentity identity, IEnumerable<UserGameProviderInfo> gameProviders)
        {
            identity.AddClaim(
                new Claim(
                    Games,
                    SerializeObject(
                        gameProviders.ToDictionary(gameProviderInfo => gameProviderInfo.Game.Name, gameProviderInfo => gameProviderInfo.PlayerId),
                        Formatting.Indented
                    ),
                    Json
                )
            );
        }
    }
}

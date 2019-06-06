// Filename: TestAuthenticationMiddleware.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Security.Hosting;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

using Newtonsoft.Json;

namespace eDoxa.Security.Authentication
{
    internal sealed class TestAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public TestAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()),
                    new Claim(JwtClaimTypes.FamilyName, "Test"),
                    new Claim(JwtClaimTypes.GivenName, "Test"),
                    new Claim(JwtClaimTypes.Name, "Test Test"),
                    new Claim(JwtClaimTypes.Nonce, Guid.NewGuid().ToString()),
                    new Claim(JwtClaimTypes.IdentityProvider, nameof(TestAuthenticationMiddleware))
                },
                EnvironmentNames.Testing
            );

            context.User = this.CreateClaimsPrincipal(context, identity);

            await _next.Invoke(context);
        }

        private ClaimsPrincipal CreateClaimsPrincipal(HttpContext context, ClaimsIdentity identity)
        {
            var newClaims = context.Request.Headers[nameof(Claim)];

            if (!StringValues.IsNullOrEmpty(newClaims))
            {
                foreach (var newClaim in JsonConvert.DeserializeObject<IDictionary<string, string>>(newClaims))
                {
                    var oldClaim = identity.Claims.SingleOrDefault(claim => claim.Type == newClaim.Key);

                    if (oldClaim != null)
                    {
                        identity.RemoveClaim(oldClaim);
                    }

                    identity.AddClaim(new Claim(newClaim.Key, newClaim.Value));
                }
            }

            return new ClaimsPrincipal(identity);
        }
    }
}

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
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Security.Hosting;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

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
            var subjet = context.Request.Headers[JwtClaimTypes.Subject];

            var user = new ClaimsIdentity(
                new[]
                {
                    new Claim(JwtClaimTypes.Subject, !StringValues.IsNullOrEmpty(subjet) ? subjet.ToString() : Guid.NewGuid().ToString()),
                    new Claim(JwtClaimTypes.Name, "Test Test"),
                    new Claim(JwtClaimTypes.IdentityProvider, nameof(TestAuthenticationMiddleware)),
                    new Claim(JwtClaimTypes.Nonce, Guid.NewGuid().ToString()),
                    new Claim(JwtClaimTypes.FamilyName, "Test"),
                    new Claim(JwtClaimTypes.GivenName, "Test")
                },
                EnvironmentName.Testing
            );

            context.User = new ClaimsPrincipal(user);

            await _next.Invoke(context);
        }
    }
}

// Filename: IgnoreAuthenticationMiddleware.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

using IdentityModel;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace eDoxa.Seedwork.Application.Middlewares
{
    public class IgnoreAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private string _userId;

        public IgnoreAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            // Gets the request path from RequestPath object.
            var pathString = context.Request.Path;

            if (pathString == "/noauth") // If the HTTP context request path is correspond to '/noauth'.
            {
                // Query the user id against HTTP context request pipeline.
                var userId = context.Request.Query["userid"];

                // If user id set it from the request query.
                if (!string.IsNullOrEmpty(userId))
                {
                    _userId = userId;
                }

                // HTTP context response options.
                context.Response.StatusCode = (int) HttpStatusCode.OK;
                context.Response.ContentType = MediaTypeNames.Text.Plain;

                // Write HTTP response (asynchronously).
                await context.Response.WriteAsync($"UserId set to {_userId}");
            }
            else if (pathString == "/noauth/reset") // If the HTTP context request path is correspond to '/noauth/reset'.
            {
                // User id set to null.
                _userId = null;

                // HTTP context response options.
                context.Response.StatusCode = (int) HttpStatusCode.OK;
                context.Response.ContentType = MediaTypeNames.Text.Plain;

                // Write HTTP response (asynchronously).
                await context.Response.WriteAsync("UserId set to null. Token required for protected endpoints.");
            }
            else // If the HTTP context request path isn't correspond to any previous conditions.
            {
                // Obtained the request headers authorization values.
                var stringValues = context.Request.Headers["Authorization"];

                // If the request headers authorization values are not empty.
                if (stringValues != StringValues.Empty)
                {
                    // Retrieve the first or default HTTP context header request value from the request headers authorization values.
                    var header = stringValues.FirstOrDefault();

                    // Ensure the HTTP context header request contains a valid email in the request.
                    if (!string.IsNullOrEmpty(header) && header.StartsWith("Email ") && header.Length > "Email ".Length)
                    {
                        // Try to retrieve the user email from the HTTP context header request.
                        var email = header.Substring("Email ".Length);

                        // Check if the email is valid.
                        if (!string.IsNullOrEmpty(email))
                        {
                            // Create and replace the default HTTP ClaimsPrincipal (User) context with a ClaimsIdentity for testing purposes.
                            context.User = new ClaimsPrincipal(
                                new ClaimsIdentity(
                                    new[]
                                    {
                                        new Claim("emails", email),
                                        new Claim("name", "Default User"),
                                        new Claim("nonce", Guid.NewGuid().ToString()),
                                        new Claim("ttp://schemas.microsoft.com/identity/claims/identityprovider", nameof(IgnoreAuthenticationMiddleware)),
                                        new Claim("nonce", Guid.NewGuid().ToString()),
                                        new Claim(JwtClaimTypes.Subject, default(Guid).ToString()),
                                        new Claim(ClaimTypes.Surname, "User"),
                                        new Claim(ClaimTypes.GivenName, "Microsoft")
                                    },
                                    "IgnoreAuthentication"
                                )
                            );
                        }
                    }
                }

                // Invokes the next middleware in the pipeline.
                await _next.Invoke(context);
            }
        }
    }
}
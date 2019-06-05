// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Security.Authentication;
using eDoxa.Security.Hosting.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Security.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCorsPolicy(this IApplicationBuilder application)
        {
            application.UseCors(CustomPolicies.CorsPolicy);
        }

        public static void UseAuthentication(this IApplicationBuilder application, IHostingEnvironment environment)
        {
            if (environment.IsTesting())
            {
                application.UseMiddleware<TestAuthenticationMiddleware>();
            }
            else
            {
                application.UseAuthentication();
            }
        }
    }
}

// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Hosting.Extensions;
using eDoxa.Seedwork.Security.Middlewares;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Seedwork.Security.Extensions
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

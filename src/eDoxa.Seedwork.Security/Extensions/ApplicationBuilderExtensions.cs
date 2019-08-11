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

using Microsoft.AspNetCore.Builder;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCorsPolicy(this IApplicationBuilder application)
        {
            application.UseCors(CustomPolicies.CorsPolicy);
        }
    }
}

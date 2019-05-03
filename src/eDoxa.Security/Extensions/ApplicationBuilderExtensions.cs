﻿// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Builder;

namespace eDoxa.Security.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCorsPolicy(this IApplicationBuilder application)
        {
            application.UseCors(CustomPolicies.CorsPolicy);
        }
    }
}
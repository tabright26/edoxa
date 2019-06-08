// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Builder;

namespace eDoxa.Web.Status.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseHealthChecksUI(this IApplicationBuilder application, string path)
        {
            application.UseHealthChecksUI(config => config.UIPath = path);

            application.UseStatusCodePagesWithRedirects(path);
        }
    }
}

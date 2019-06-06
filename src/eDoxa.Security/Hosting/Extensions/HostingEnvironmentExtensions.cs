// Filename: HostingEnvironmentExtensions.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Security.Hosting.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsTesting(this IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostingEnvironment));
            }

            return hostingEnvironment.IsEnvironment(EnvironmentNames.Testing);
        }
    }
}

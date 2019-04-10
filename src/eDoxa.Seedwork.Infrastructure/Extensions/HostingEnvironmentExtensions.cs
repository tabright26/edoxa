// Filename: HostingEnvironmentExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        private const string Testing = nameof(Testing);

        public static bool IsTesting(this IHostingEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsEnvironment(Testing);
        }
    }
}
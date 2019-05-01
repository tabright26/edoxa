// Filename: IdentityHostingStartup.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.IdentityServer.Areas.Identity;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace eDoxa.IdentityServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure([NotNull] IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}
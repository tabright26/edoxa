// Filename: TestIdentityStartup.cs
// Date Created: 2019-07-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Api;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Identity.IntegrationTests.Helpers
{
    public class TestIdentityStartup : Startup
    {
        public TestIdentityStartup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }
    }
}

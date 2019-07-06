// Filename: ArenaChallengesStartup.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace eDoxa.FunctionalTests.Services.Arena.Challenges.Helpers
{
    public class ArenaChallengesStartup : Startup
    {
        public ArenaChallengesStartup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }
    }
}

// Filename: TestStartup.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers.Mocks;
using eDoxa.Arena.LeagueOfLegends.Abstractions;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
{
    internal class TestArenaChallengesStartup : Startup
    {
        public TestArenaChallengesStartup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration, environment)
        {
        }

        protected override IServiceProvider BuildModule(IServiceCollection services)
        {
            services.AddTransient<ILeagueOfLegendsService, MockLeagueOfLegendsService>();

            return services.Build<TestArenaChallengesModule>();
        }
    }
}

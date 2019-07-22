// Filename: TestArenaChallengesStartup.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
        public TestArenaChallengesStartup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) : base(configuration, hostingEnvironment)
        {
        }

        protected override IServiceProvider BuildModule(IServiceCollection services)
        {
            services.AddTransient<ILeagueOfLegendsService, MockLeagueOfLegendsService>();

            return services.Build<TestArenaChallengesModule>();
        }
    }
}

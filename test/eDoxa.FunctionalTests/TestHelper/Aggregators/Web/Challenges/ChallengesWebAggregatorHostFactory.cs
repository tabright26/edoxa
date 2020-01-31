// Filename: ChallengesWebAggregatorHostFactory.cs
// Date Created: 2019-12-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Challenges.Web.Aggregator;
using eDoxa.Seedwork.TestHelper;
using eDoxa.Seedwork.TestHelper.Extensions;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.FunctionalTests.TestHelper.Aggregators.Web.Challenges
{
    public sealed class ChallengesWebAggregatorHostFactory : WebHostFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseCustomContentRoot("TestHelper/Aggregators/Web/Challenges");
        }
    }
}

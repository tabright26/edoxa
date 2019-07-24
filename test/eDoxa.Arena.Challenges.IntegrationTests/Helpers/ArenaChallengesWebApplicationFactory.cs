// Filename: TestArenaChallengesWebApplicationFactory.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers.Mocks;
using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security.AzureKeyVault.Extensions;
using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Helpers;

using JetBrains.Annotations;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
{
    public class ArenaChallengesWebApplicationFactory : CustomWebApplicationFactory<Program>
    {
        public ArenaChallengesWebApplicationFactory()
        {
            Startup.ConfigureContainerBuilder += builder =>
            {
                builder.RegisterType<MockLeagueOfLegendsService>().As<ILeagueOfLegendsService>().InstancePerDependency();

                builder.RegisterType<MockIntegrationEventService>().As<IIntegrationEventService>().InstancePerDependency();
            };
        }

        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing).UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location));
        }

        [NotNull]
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder<Startup>(Array.Empty<string>()).UseAzureKeyVault().UseSerilog();
        }

        [NotNull]
        protected override TestServer CreateServer([NotNull] IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.EnsureCreatedDbContext<ArenaChallengesDbContext>();

            return server;
        }

        public override void WithContainerBuilder(Action<ContainerBuilder> builder)
        {
            Startup.ConfigureContainerBuilder += builder;
        }
    }
}

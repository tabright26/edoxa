// Filename: ArenaChallengesWebApplicationFactory.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers.Mocks;
using eDoxa.Seedwork.IntegrationEvents;
using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Seedwork.Testing.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

using Moq;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
{
    public class ArenaChallengesWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing);

            builder.UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(ArenaChallengesWebApplicationFactory)).Location));

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());

            builder.ConfigureTestContainer<ContainerBuilder>(
                container =>
                {
                    container.RegisterType<MockLeagueOfLegendsService>().As<ILeagueOfLegendsService>().InstancePerDependency();
                    container.RegisterType<MockIntegrationEventService>().As<IIntegrationEventService>().InstancePerDependency();
                    var mock = new Mock<IIdentityService>();

                    mock.Setup(identityService => identityService.HasGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>())).ReturnsAsync(true);

                    mock.Setup(identityService => identityService.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()))
                        .ReturnsAsync(new GameAccountId(Guid.NewGuid().ToString()));

                    container.RegisterInstance(mock.Object).As<IIdentityService>();
                }
            );
        }

        [NotNull]
        protected override TestServer CreateServer([NotNull] IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.EnsureCreatedDbContext<ArenaChallengesDbContext>();

            return server;
        }
    }
}

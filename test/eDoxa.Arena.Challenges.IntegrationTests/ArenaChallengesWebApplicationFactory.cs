// Filename: ArenaChallengesWebApplicationFactory.cs
// Date Created: 2019-07-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Dtos;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.IntegrationEvents;
using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Seedwork.Testing.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

using Moq;

namespace eDoxa.Arena.Challenges.IntegrationTests
{
    public sealed class ArenaChallengesWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing);

            builder.UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(ArenaChallengesWebApplicationFactory)).Location));

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());

            builder.ConfigureTestContainer<ContainerBuilder>(
                container =>
                {
                    var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();

                    mockLeagueOfLegendsService
                        .Setup(
                            leagueOfLegendsService => leagueOfLegendsService.GetMatchReferencesAsync(
                                It.IsAny<string>(),
                                It.IsAny<DateTime>(),
                                It.IsAny<DateTime>()
                            )
                        )
                        .ReturnsAsync(Array.Empty<LeagueOfLegendsMatchReferenceDto>());

                    mockLeagueOfLegendsService.Setup(leagueOfLegendsService => leagueOfLegendsService.GetMatchAsync(It.IsAny<string>()))
                        .ReturnsAsync(new LeagueOfLegendsMatchDto());

                    container.RegisterInstance(mockLeagueOfLegendsService.Object).As<ILeagueOfLegendsService>().SingleInstance();

                    var mockIntegrationEventService = new Mock<IIntegrationEventService>();

                    mockIntegrationEventService.Setup(integrationEventService => integrationEventService.PublishAsync(It.IsAny<IntegrationEvent>()))
                        .Returns(Task.CompletedTask);

                    container.RegisterInstance(mockIntegrationEventService.Object).As<IIntegrationEventService>().SingleInstance();
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

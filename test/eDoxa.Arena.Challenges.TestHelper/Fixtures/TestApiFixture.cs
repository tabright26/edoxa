// Filename: TestApiFixture.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Dtos;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.TestHelper;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.ServiceBus.Moq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

using Moq;

namespace eDoxa.Arena.Challenges.TestHelper.Fixtures
{
    public sealed class TestApiFixture : WebApiFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(TestApiFixture)).Location));

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
                                It.IsAny<DateTime>()))
                        .ReturnsAsync(Array.Empty<LeagueOfLegendsMatchReferenceDto>());

                    mockLeagueOfLegendsService.Setup(leagueOfLegendsService => leagueOfLegendsService.GetMatchAsync(It.IsAny<string>()))
                        .ReturnsAsync(new LeagueOfLegendsMatchDto());

                    container.RegisterInstance(mockLeagueOfLegendsService.Object).As<ILeagueOfLegendsService>().SingleInstance();

                    container.RegisterModule<MockServiceBusModule>();
                });
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.EnsureCreatedDbContext<ArenaChallengesDbContext>();

            return server;
        }
    }
}

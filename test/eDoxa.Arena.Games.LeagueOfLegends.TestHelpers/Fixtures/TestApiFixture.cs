﻿// Filename: TestApiFixture.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Arena.Games.LeagueOfLegends.Api;
using eDoxa.Seedwork.Testing;
using eDoxa.ServiceBus.Moq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Arena.Games.LeagueOfLegends.TestHelpers.Fixtures
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
                    container.RegisterModule<MockServiceBusModule>();
                });
        }
    }
}
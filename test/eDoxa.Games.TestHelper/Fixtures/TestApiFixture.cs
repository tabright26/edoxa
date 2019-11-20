﻿// Filename: TestApiFixture.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Games.Api;
using eDoxa.Games.Infrastructure;
using eDoxa.Seedwork.TestHelper;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.ServiceBus.Moq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Games.TestHelper.Fixtures
{
    public sealed class TestApiFixture : WebApiFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(TestApiFixture)).Location));

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());

            base.ConfigureWebHost(builder);
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
        }

        protected override void ContainerTestBuilder(ContainerBuilder builder)
        {
            builder.RegisterModule<MockServiceBusModule>();
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.EnsureCreatedDbContext<GamesDbContext>();

            return server;
        }
    }
}
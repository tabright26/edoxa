// Filename: TestApiFactory.cs
// Date Created: 2019-09-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Identity.Api;
using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.TestHelpers.Mocks;
using eDoxa.Seedwork.Testing;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.ServiceBus.Moq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Identity.TestHelpers
{
    public sealed class TestApiFactory : IdentityApiFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(TestApiFactory)).Location));

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());

            builder.ConfigureTestContainer<ContainerBuilder>(
                container =>
                {
                    var mockEmailSender = new MockEmailSender();

                    container.RegisterInstance(mockEmailSender.Object).As<IEmailSender>().SingleInstance();

                    container.RegisterModule<MockServiceBusModule>();
                });
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.MigrateDbContext<IdentityDbContext>();

            return server;
        }
    }
}

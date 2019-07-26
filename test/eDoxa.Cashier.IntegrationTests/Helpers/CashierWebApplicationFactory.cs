// Filename: TestCashieWebApplicationFactory.cs
// Date Created: 2019-07-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Cashier.Api;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.IntegrationTests.Helpers.Mocks;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.IntegrationEvents;
using eDoxa.Seedwork.Security.AzureKeyVault.Extensions;
using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Helpers;

using JetBrains.Annotations;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Cashier.IntegrationTests.Helpers
{
    public class CashierWebApplicationFactory : CustomWebApplicationFactory<Program>
    {
        public CashierWebApplicationFactory()
        {
            Startup.ConfigureContainerBuilder += builder =>
            {
                builder.RegisterType<MockIntegrationEventService>().As<IIntegrationEventService>().InstancePerDependency();
            };
        }

        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing)
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(CashierWebApplicationFactory)).Location))
                .ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());
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

            server.EnsureCreatedDbContext<CashierDbContext>();

            return server;
        }

        public override void WithContainerBuilder(Action<ContainerBuilder> builder)
        {
            Startup.ConfigureContainerBuilder += builder;
        }
    }
}

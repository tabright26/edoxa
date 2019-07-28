﻿// Filename: CashierWebApplicationFactory.cs
// Date Created: 2019-07-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Cashier.Api;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.IntegrationEvents;
using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Seedwork.Testing.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

using Moq;

namespace eDoxa.Cashier.IntegrationTests
{
    public sealed class CashierWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing);

            builder.UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(CashierWebApplicationFactory)).Location));

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());

            builder.ConfigureTestContainer<ContainerBuilder>(
                container =>
                {
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

            server.EnsureCreatedDbContext<CashierDbContext>();

            return server;
        }
    }
}

// Filename: CashierApiFactory.cs
// Date Created: 2019-11-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using Autofac;

using eDoxa.Cashier.Api;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.TestHelper;
using eDoxa.Seedwork.TestHelper.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.FunctionalTests.Cashier
{
    public sealed class CashierHostFactory : WebHostFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(CashierHostFactory)).Location), "Cashier"));

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());

            base.ConfigureWebHost(builder);
        }

        protected override void ConfigureTestServices(IServiceCollection services)
        {
        }

        protected override void ContainerTestBuilder(ContainerBuilder builder)
        {
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.EnsureCreatedDbContext<CashierDbContext>();

            return server;
        }
    }
}

﻿// Filename: CashierWebApplicationFactory.cs
// Date Created: 2019-07-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Cashier.Api;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Testing;
using eDoxa.Seedwork.Testing.Extensions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace eDoxa.FunctionalTests.Services.Cashier
{
    public sealed class CashierWebApplicationFactory : CustomWebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost( IWebHostBuilder builder)
        {
            builder.UseContentRoot(
                Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(CashierWebApplicationFactory)).Location), "Services/Cashier")
            );

            builder.ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.json", false).AddEnvironmentVariables());
        }

        
        protected override TestServer CreateServer( IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.EnsureCreatedDbContext<CashierDbContext>();

            return server;
        }
    }
}

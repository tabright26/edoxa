// Filename: WebApplicationFactory.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IO;
using System.Reflection;

using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security.AzureKeyVault.Extensions;
using eDoxa.Seedwork.Security.Hosting;

using JetBrains.Annotations;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Testing
{
    public class WebApplicationFactory<TDbContext, TStartup, TProgram> : WebApplicationFactory<TProgram>
    where TDbContext : DbContext
    where TStartup : class
    where TProgram : class
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing);

            builder.UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(TProgram)).Location));
        }

        [NotNull]
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder<TStartup>(Array.Empty<string>()).UseAzureKeyVault().UseSerilog();
        }

        [NotNull]
        protected override TestServer CreateServer([NotNull] IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            using (var scope = server.Host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;

                var context = provider.GetService<TDbContext>();

                context.Database.EnsureCreated();
            }

            return server;
        }
    }
}

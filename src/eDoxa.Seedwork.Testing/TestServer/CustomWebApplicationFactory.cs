// Filename: CustomWebApplicationFactory.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Security.Hosting;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.Testing.TestServer
{
    public sealed class CustomWebApplicationFactory<TDbContext, TStartup> : WebApplicationFactory<TStartup>
    where TDbContext : DbContext
    where TStartup : class
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing);
        }

        [NotNull]
        protected override Microsoft.AspNetCore.TestHost.TestServer CreateServer([NotNull] IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            server.MigrateDbContext<TDbContext>();

            return server;
        }
    }
}

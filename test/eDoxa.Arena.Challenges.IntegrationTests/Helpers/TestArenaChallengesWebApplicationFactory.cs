﻿// Filename: WebApplicationFactory.cs
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

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security.AzureKeyVault.Extensions;
using eDoxa.Seedwork.Security.Hosting;

using JetBrains.Annotations;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Arena.Challenges.IntegrationTests.Helpers
{
    internal sealed class TestArenaChallengesWebApplicationFactory<TStartup> : WebApplicationFactory<Program>
    where TStartup : TestArenaChallengesStartup
    {
        protected override void ConfigureWebHost([NotNull] IWebHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentNames.Testing).UseContentRoot(Path.GetDirectoryName(Assembly.GetAssembly(typeof(TStartup)).Location));
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
                var challengesDbContext = scope.GetService<ChallengesDbContext>();

                challengesDbContext.Database.EnsureCreated();
            }

            return server;
        }
    }
}

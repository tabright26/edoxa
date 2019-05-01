// Filename: Program.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IdentityServer.Data;
using eDoxa.IdentityServer.Models;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.IdentityServer
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Log.Information("Building {Application} web host...");

                var host = CreateWebHostBuilder(args).Build();

                Log.Information("Applying {Application} context migrations...");

                host.MigrateDbContext<IdentityServerDbContext>(
                    (context, provider) =>
                    {
                        var environment = provider.GetService<IHostingEnvironment>();

                        if (environment.IsDevelopment())
                        {
                            var userManager = provider.GetService<UserManager<User>>();

                            if (!userManager.Users.Any())
                            {
                                var user = new User
                                {
                                    Id = Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"),
                                    Email = "admin@edoxa.gg",
                                    EmailConfirmed = true,
                                    NormalizedEmail = "admin@edoxa.gg".ToUpperInvariant(),
                                    UserName = "Administrator",
                                    NormalizedUserName = "Administrator".ToUpperInvariant(),
                                    PhoneNumber = "5147580313",
                                    PhoneNumberConfirmed = true,
                                    SecurityStamp = Guid.NewGuid().ToString("D")
                                };

                                userManager.CreateAsync(user, "Pass@word1").Wait();
                            }
                        }
                    }
                );

                Log.Information("Starting {Application} web host...");

                host.Run();

                return 0;
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Program '{Application}' exited with code 1.");

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder<Startup>(args)
                .CaptureStartupErrors(false)
                .ConfigureLogging()
                .UseAzureKeyVault()
                .UseApplicationInsights()
                .UseSerilog();
        }
    }
}
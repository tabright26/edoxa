﻿// Filename: Program.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Security.Claims;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Infrastructure;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.ServiceBus;

using IdentityModel;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace eDoxa.Identity.Api
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Log.Information("Building {Application} web host...");

                var host = CreateWebHostBuilder(args).Build();

                Log.Information("Applying {Application} context migrations...");

                host.MigrateDbContext<IdentityDbContext>(
                    (context, provider) =>
                    {
                        var environment = provider.GetService<IHostingEnvironment>();

                        if (environment.IsDevelopment())
                        {
                            var userManager = provider.GetService<UserManager<User>>();

                            var roleManager = provider.GetService<RoleManager<Role>>();

                            if (!userManager.Users.Any())
                            {
                                var user = new User
                                {
                                    Id = Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"),
                                    UserName = "Administrator",
                                    Email = "admin@edoxa.gg",
                                    EmailConfirmed = true,
                                    PhoneNumber = "5147580313",
                                    PhoneNumberConfirmed = true
                                };

                                userManager.CreateAsync(user, "Pass@word1").Wait();

                                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.GivenName, "Francis")).Wait();

                                userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.FamilyName, "Quenneville")).Wait();

                                userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.CustomerId, "cus_Et3R5o75SIURKE")).Wait();

                                userManager.AddClaimAsync(
                                    user,
                                    new Claim(
                                        JwtClaimTypes.BirthDate,
                                        new DateTime(1995, 5, 6).ToString("yyyy-MM-dd")
                                    )
                                ).Wait();

                                userManager.AddClaimAsync(
                                    user,
                                    new Claim(
                                        Game.LeagueOfLegends.GetClaimType(),
                                        "NzH50JS-LCAu0UEY4EMjuS710F_U_8pLfEpNib9X06dD4w"
                                    )
                                ).Wait();

                                var admin = new Role
                                {
                                    Name = "Admin",
                                    NormalizedName = "Admin".ToUpperInvariant()
                                };

                                roleManager.CreateAsync(admin).Wait();

                                roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "account.deposit")).Wait();

                                roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "account.withdraw")).Wait();

                                userManager.AddToRoleAsync(user, admin.Name).Wait();

                                var regular = new Role
                                {
                                    Name = "Regular",
                                    NormalizedName = "Regular".ToUpperInvariant()
                                };

                                roleManager.CreateAsync(regular).Wait();

                                roleManager.AddClaimAsync(regular, new Claim(CustomClaimTypes.Permission, "account.deposit")).Wait();

                                userManager.AddToRoleAsync(user, regular.Name).Wait();
                            }
                        }

                        //var environment = provider.GetService<IHostingEnvironment>();

                        //if (environment.IsDevelopment())
                        //{
                        //    var factory = provider.GetService<ILoggerFactory>();

                        //    var task = context.SeedAsync(factory.CreateLogger<IdentityDbContext>());

                        //    task.Wait();
                        //}
                    }
                );

                host.MigrateDbContext<IntegrationEventLogDbContext>(
                    (context, provider) => { }
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
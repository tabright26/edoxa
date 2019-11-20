// Filename: Startup.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac;

using eDoxa.Challenges.Web.Jobs.Filters;
using eDoxa.Challenges.Web.Jobs.Infrastructure;
using eDoxa.Challenges.Web.Jobs.Services;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.ServiceBus.Azure.Modules;

using Hangfire;
using Hangfire.SqlServer;

using HealthChecks.UI.Client;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace eDoxa.Challenges.Web.Jobs
{
    public class Startup
    {
        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
        }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<HangfireOptions>(Configuration);

            services.AddHealthChecks()
                .AddCheck("liveness", () => HealthCheckResult.Healthy())
                .AddAzureKeyVault(Configuration)
                .AddAzureServiceBusTopic(Configuration);

            services.AddDbContext<HangfireDbContext>(
                builder => builder.UseSqlServer(
                    Configuration.GetSqlServerConnectionString()!,
                    options => options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));

            // Add Hangfire services.
            services.AddHangfire(
                configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(
                        Configuration.GetSqlServerConnectionString()!,
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            UsePageLocksOnDequeue = true,
                            DisableGlobalLocks = true
                        }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AzureServiceBusModule<Startup>(Configuration.GetAzureServiceBusConnectionString()!, AppNames.ChallengesWebJobs));
        }

        public void Configure(IApplicationBuilder application, IRecurringJobManager recurringJobManager, IOptions<HangfireOptions> options)
        {
            recurringJobManager.AddOrUpdate<ChallengeService>(
                Game.LeagueOfLegends.ToString(),
                service => service.SynchronizeChallengesAsync(Game.LeagueOfLegends),
                options.Value.RecurringJobs[Game.LeagueOfLegends.ToString()]);

            application.UseHangfireServer();

            application.UseHangfireDashboard(
                string.Empty,
                new DashboardOptions
                {
                    Authorization = new[] {new AllowAnonymousAuthorizationFilter()}
                });

            application.UseMvc();

            application.UseHealthChecks(
                "/liveness",
                new HealthCheckOptions
                {
                    Predicate = registration => registration.Name.Contains("liveness")
                });

            application.UseHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
        }
    }
}

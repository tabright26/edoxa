// Filename: Startup.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac;

using eDoxa.Challenges.Workers.Application.Extensions;
using eDoxa.Challenges.Workers.Application.RecurringJobs;
using eDoxa.Challenges.Workers.Infrastructure;
using eDoxa.Grpc.Protos.Challenges.Services;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Games.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Monitoring.HealthChecks.Extensions;
using eDoxa.Seedwork.Security.DataProtection.Extensions;

using Grpc.Core;

using Hangfire;
using Hangfire.SqlServer;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Challenges.Workers
{
    public partial class Startup
    {
        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true); // TODO: Check for security is required.
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = configuration.GetAppSettings<ChallengesWorkersAppSettings>();
        }

        public IConfiguration Configuration { get; }

        private ChallengesWorkersAppSettings AppSettings { get; }
    }

    public partial class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddCustomAzureKeyVault(Configuration)
                .AddCustomRedis(Configuration)
                .AddCustomSqlServer(Configuration)
                .AddCustomUrlGroup(AppSettings.Endpoints.ChallengesUrl, AppServices.ChallengesApi)
                .AddCustomUrlGroup(AppSettings.Endpoints.GamesUrl, AppServices.GamesApi);

            services.AddDbContext<HangfireDbContext>(
                builder => builder.UseSqlServer(
                    Configuration.GetSqlServerConnectionString(),
                    options => options.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null)));

            services.AddCustomDataProtection(Configuration, AppServices.ChallengesWorkers);

            services.AddHangfire(
                configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(
                        Configuration.GetSqlServerConnectionString(),
                        new SqlServerStorageOptions
                        {
                            SchemaName = "challenges",
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            UsePageLocksOnDequeue = true,
                            DisableGlobalLocks = true
                        }));

            services.AddHangfireServer(options => options.WorkerCount = 1);

            services.AddMvc();

            services.AddGrpcClient<GameService.GameServiceClient>(options => options.Address = new Uri($"{AppSettings.Endpoints.GamesUrl}:81"))
                .ConfigureChannel(options => options.Credentials = ChannelCredentials.Insecure)
                .AddRetryPolicyHandler()
                .AddCircuitBreakerPolicyHandler();

            services.AddGrpcClient<ChallengeService.ChallengeServiceClient>(options => options.Address = new Uri($"{AppSettings.Endpoints.ChallengesUrl}:81"))
                .ConfigureChannel(options =>
                {
                    options.Credentials = ChannelCredentials.Insecure;
                    options.MaxReceiveMessageSize = 1000000000;
                })
                .AddRetryPolicyHandler()
                .AddCircuitBreakerPolicyHandler();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseCustomPathBase();

            application.UseRouting();

            application.UseEndpoints(endpoints => endpoints.MapCustomHealthChecks());

            application.UseCustomHangfireDashboard();
        }
    }

    public partial class Startup
    {
        public void ConfigureProduction(IApplicationBuilder application)
        {
            this.Configure(application);

            application.UseHangfireRecurringJobs(
                manager =>
                {
                    manager.AddOrUpdate<ChallengeRecurringJob>(
                        EnumGame.LeagueOfLegends.ToString(),
                        service => service.SynchronizeChallengesAsync(EnumGame.LeagueOfLegends),
                        Cron.Hourly);
                });
        }
    }
}

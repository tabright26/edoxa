// Filename: Startup.cs
// Date Created: 2019-04-12
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Autofac.Extensions;
using eDoxa.Challenges.BackgroundTasks.Extensions;
using eDoxa.Challenges.BackgroundTasks.Infrastructure;
using eDoxa.Challenges.BackgroundTasks.Settings;
using eDoxa.Monitoring.Extensions;
using eDoxa.ServiceBus.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Challenges.BackgroundTasks
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<ChallengePublishingSettings>(Configuration.GetSection(nameof(ChallengePublishingSettings)));

            services.Configure<ChallengeSynchronizingSettings>(Configuration.GetSection(nameof(ChallengeSynchronizingSettings)));

            services.Configure<ChallengeClosingSettings>(Configuration.GetSection(nameof(ChallengeClosingSettings)));

            services.AddServiceBus(Configuration);

            services.AddEventBus(Configuration);

            services.AddHealthChecks(Configuration);

            return services.Build<ChallengesModule>();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();
        }
    }
}
// Filename: ChallengesModule.cs
// Date Created: 2019-03-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.Challenges.BackgroundTasks.Services;

using Microsoft.Extensions.Hosting;

namespace eDoxa.Challenges.BackgroundTasks.Infrastructure
{
    public sealed class ChallengesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChallengePublishingBackgroundService>().As<IHostedService>().SingleInstance();

            builder.RegisterType<ChallengeSynchronizingBackgroundService>().As<IHostedService>().SingleInstance();

            builder.RegisterType<ChallengeClosingBackgroundService>().As<IHostedService>().SingleInstance();
        }
    }
}
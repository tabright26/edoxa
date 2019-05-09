// Filename: ChallengesExtensions.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Application.IntegrationEvents;
using eDoxa.Challenges.Application.IntegrationEvents.Handlers;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Challenges.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IApplicationBuilder application)
        {
            var service = application.ApplicationServices.GetRequiredService<IEventBusService>();
            
            service.Subscribe<ChallengePublishedIntegrationEvent, ChallengePublishedIntegrationEventHandler>();

            service.Subscribe<ChallengeSynchronizedIntegrationEvent, ChallengeSynchronizedIntegrationEventHandler>();

            service.Subscribe<ChallengeCompletedIntegrationEvent, ChallengeCompletedIntegrationEventHandler>();
        }
    }
}
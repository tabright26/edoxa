// Filename: ChallengePublishedIntegrationEvent.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Entities;
using eDoxa.ServiceBus;

namespace eDoxa.Challenges.Application.IntegrationEvents
{
    public sealed class ChallengePublishedIntegrationEvent : IntegrationEvent
    {
        public ChallengePublishedIntegrationEvent(PublisherInterval interval)
        {
            Interval = interval;
        }

        public PublisherInterval Interval { get; set; }
    }
}
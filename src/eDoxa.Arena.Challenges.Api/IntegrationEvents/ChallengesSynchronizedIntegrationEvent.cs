// Filename: ChallengesSynchronizedIntegrationEvent.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Arena.Challenges.Api.IntegrationEvents
{
    public sealed class ChallengesSynchronizedIntegrationEvent : IIntegrationEvent
    {
        public string Name => IntegrationEventNames.ArenaChallengesSynchronized;
    }
}

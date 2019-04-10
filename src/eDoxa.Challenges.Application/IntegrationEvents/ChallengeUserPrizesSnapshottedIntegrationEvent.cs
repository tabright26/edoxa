// Filename: ChallengeUserPrizesSnapshottedIntegrationEvent.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.ServiceBus;

namespace eDoxa.Challenges.Application.IntegrationEvents
{
    public sealed class ChallengeUserPrizesSnapshottedIntegrationEvent : IntegrationEvent
    {
        public ChallengeUserPrizesSnapshottedIntegrationEvent(Guid challengeId, IReadOnlyDictionary<Guid, decimal?> userPrizes)
        {
            ChallengeId = challengeId;
            UserPrizes = userPrizes;
        }

        public Guid ChallengeId { get; private set; }

        public IReadOnlyDictionary<Guid, decimal?> UserPrizes { get; private set; }
    }
}
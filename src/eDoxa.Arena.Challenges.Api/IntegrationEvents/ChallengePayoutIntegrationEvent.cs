// Filename: ChallengePayoutIntegrationEvent.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.IntegrationEvents;

namespace eDoxa.Arena.Challenges.Api.IntegrationEvents
{
    public sealed class ChallengePayoutIntegrationEvent : IntegrationEvent
    {
        public ChallengePayoutIntegrationEvent(Guid challengeId, IReadOnlyDictionary<Guid, decimal?> userPrizes)
        {
            ChallengeId = challengeId;
            UserPrizes = userPrizes;
        }

        public Guid ChallengeId { get; private set; }

        public IReadOnlyDictionary<Guid, decimal?> UserPrizes { get; private set; }
    }
}

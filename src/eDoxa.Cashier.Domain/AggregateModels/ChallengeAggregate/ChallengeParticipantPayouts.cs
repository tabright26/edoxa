// Filename: ChallengeParticipantPayouts.cs
// Date Created: 2020-02-10
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeParticipantPayouts : Dictionary<UserId, ChallengePayoutBucketPrize>
    {
        public ChallengeParticipantPayouts(ChallengeScoreboard scoreboard) : base(
            scoreboard.ToDictionary(participant => participant.Key, participant => ChallengePayoutBucketPrize.None))
        {
        }
    }
}

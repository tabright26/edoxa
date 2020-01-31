// Filename: ChallengeParticipantPayouts.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeParticipantPayouts : Dictionary<UserId, ChallengePayoutBucketPrize>
    {
    }
}

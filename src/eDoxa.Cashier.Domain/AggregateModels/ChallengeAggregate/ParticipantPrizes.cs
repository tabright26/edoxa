// Filename: ParticipantPrizes.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ParticipantPrizes : Dictionary<UserId, Prize>, IParticipantPrizes
    {
    }
}

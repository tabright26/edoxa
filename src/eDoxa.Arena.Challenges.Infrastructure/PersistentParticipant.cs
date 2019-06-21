// Filename: PersistentParticipant.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Arena.Challenges.Infrastructure
{
    internal sealed class PersistentParticipant : Participant
    {
        public PersistentParticipant(
            ParticipantId participantId,
            UserId userId,
            GameAccountId gameAccountId,
            IDateTimeProvider provider = null
        ) : base(userId, gameAccountId, provider)
        {
            this.SetEntityId(participantId);
        }
    }
}

// Filename: PersistentChallenge.cs
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

namespace eDoxa.Arena.Challenges.Infrastructure.PersistentModels
{
    internal sealed class PersistentChallenge : Challenge
    {
        public PersistentChallenge(
            ChallengeId challengeId,
            ChallengeGame game,
            ChallengeName name,
            ChallengeSetup setup,
            ChallengeDuration duration,
            IDateTimeProvider provider
        ) : base(
            game,
            name,
            setup,
            duration,
            provider
        )
        {
            this.SetEntityId(challengeId);
        }
    }
}

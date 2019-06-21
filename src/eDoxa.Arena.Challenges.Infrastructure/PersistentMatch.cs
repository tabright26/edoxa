// Filename: PersistentMatch.cs
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

namespace eDoxa.Arena.Challenges.Infrastructure
{
    internal sealed class PersistentMatch : Match
    {
        public PersistentMatch(MatchId matchId, GameMatchId gameMatchId, IDateTimeProvider provider = null) : base(gameMatchId, provider)
        {
            this.SetEntityId(matchId);
        }
    }
}

// Filename: ChallengeData.cs
// Date Created: 2019-06-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeData
    {
        private readonly IChallenge _challenge;

        public ChallengeData(IChallenge challenge)
        {
            _challenge = challenge;
        }

        public ChallengeId Id => _challenge.Id;

        public Game Game => _challenge.Game;

        public ChallengeName Name => _challenge.Name;

        public ChallengeSetup Setup => _challenge.Setup;

        public ChallengeTimeline Timeline => _challenge.Timeline;

        public ChallengeState State => _challenge.Timeline.State;

        public DateTime CreatedAt => _challenge.CreatedAt;

        public IPayout Payout => new Payout(_challenge.Buckets as IBuckets);

        public IScoreboard Scoreboard => new Scoreboard(_challenge);
        
        public IScoring Scoring => new Scoring(_challenge.Stats);

        public IReadOnlyCollection<Participant> Participants => _challenge.Participants;
    }
}

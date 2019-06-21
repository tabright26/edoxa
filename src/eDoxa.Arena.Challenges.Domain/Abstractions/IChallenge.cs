// Filename: IChallenge.cs
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
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.Abstractions
{
    public interface IChallenge : IEntity<ChallengeId>, IAggregateRoot
    {
        DateTime CreatedAt { get; }

        DateTime? SynchronizedAt { get; }

        ChallengeName Name { get; }

        ChallengeGame Game { get; }

        ChallengeSetup Setup { get; }

        ChallengeTimeline Timeline { get; }

        ChallengeState State { get; }

        IScoring Scoring { get; }

        IPayout Payout { get; }

        IScoreboard Scoreboard { get; }

        IReadOnlyCollection<Participant> Participants { get; }

        Task SynchronizeAsync(IGameMatchIdsFactory gameMatchIdsFactory, IMatchStatsFactory matchStatsFactory);

        Participant Register(Participant participant);
    }
}

// Filename: IChallenge.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels
{
    public interface IChallenge : IEntity<ChallengeId>, IAggregateRoot
    {
        ChallengeName Name { get; }

        Game Game { get; }

        BestOf BestOf { get; }

        Entries Entries { get; }

        ChallengeTimeline Timeline { get; }

        DateTime? SynchronizedAt { get; }

        IScoring Scoring { get; }

        IScoreboard Scoreboard { get; }

        IReadOnlyCollection<Participant> Participants { get; }

        void Register(Participant participant);

        void Start(IDateTimeProvider startedAt);

        void Close(IDateTimeProvider closedAt);

        void Synchronize(
            Func<GameAccountId, DateTime, DateTime, IEnumerable<GameReference>> getGameReferences,
            Func<GameAccountId, GameReference, IScoring, IMatch> getMatch,
            IDateTimeProvider synchronizedAt
        );

        void Synchronize(IDateTimeProvider synchronizedAt);

        bool IsInscriptionCompleted();
    }
}

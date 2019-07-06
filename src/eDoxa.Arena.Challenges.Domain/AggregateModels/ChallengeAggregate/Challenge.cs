// Filename: Challenge.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.Validators;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Challenge : Entity<ChallengeId>, IChallenge
    {
        private readonly HashSet<Participant> _participants = new HashSet<Participant>();

        public Challenge(
            ChallengeName name,
            ChallengeGame game,
            ChallengeSetup setup,
            ChallengeTimeline timeline,
            IScoring scoring,
            IPayout payout
        )
        {
            Name = name;
            Game = game;
            Setup = setup;
            Timeline = timeline;
            Scoring = scoring;
            Payout = payout;
        }

        public ChallengeName Name { get; }

        public ChallengeGame Game { get; }

        public ChallengeSetup Setup { get; }

        public ChallengeTimeline Timeline { get; private set; }

        public DateTime? SynchronizedAt { get; private set; }

        public IScoring Scoring { get; }

        public IPayout Payout { get; }

        public IReadOnlyCollection<Participant> Participants => _participants;

        public IScoreboard Scoreboard => new Scoreboard(this);

        public void Register(Participant participant)
        {
            if (!this.CanRegister(participant))
            {
                throw new InvalidOperationException();
            }

            _participants.Add(participant);
        }

        private bool CanRegister(Participant participant)
        {
            return new RegisterParticipantValidator(participant.UserId).Validate(this).IsValid;
        }

        public void Start(IDateTimeProvider startedAt)
        {
            if (!this.CanStart())
            {
                throw new InvalidOperationException();
            }

            Timeline = Timeline.Start(startedAt);
        }

        private bool CanStart()
        {
            return Participants.Count == Setup.Entries;
        }

        public void Close(IDateTimeProvider closedAt)
        {
            if (!this.CanClose())
            {
                throw new InvalidOperationException();
            }

            Timeline = Timeline.Close(closedAt);
        }

        private bool CanClose()
        {
            return Timeline == ChallengeState.Ended;
        }

        public void Synchronize(
            Func<GameAccountId, DateTime, DateTime, IEnumerable<GameReference>> getGameReferences,
            Func<GameAccountId, GameReference, IMatchStats> getMatchStats,
            IDateTimeProvider synchronizedAt
        )
        {
            if (!this.CanSynchronize())
            {
                throw new InvalidOperationException();
            }

            foreach (var participant in Participants)
            {
                var gameReferences = getGameReferences(
                    participant.GameAccountId,
                    // ReSharper disable once PossibleInvalidOperationException
                    Timeline.StartedAt.Value,
                    // ReSharper disable once PossibleInvalidOperationException
                    Timeline.EndedAt.Value
                );

                foreach (var gameReference in participant.GetUnsynchronizedGameReferences(gameReferences))
                {
                    var match = new Match(gameReference, synchronizedAt);

                    var matchStats = getMatchStats(participant.GameAccountId, gameReference);

                    match.Snapshot(matchStats, Scoring);

                    participant.Snapshot(match);
                }
            }

            this.Synchronize(synchronizedAt);
        }

        public void Synchronize(IDateTimeProvider synchronizedAt)
        {
            SynchronizedAt = synchronizedAt.DateTime;
        }

        private bool CanSynchronize()
        {
            return Timeline != ChallengeState.Inscription && Timeline.EndedAt > SynchronizedAt;
        }
    }

    public partial class Challenge : IEquatable<IChallenge>
    {
        public bool Equals([CanBeNull] IChallenge challenge)
        {
            return Id.Equals(challenge?.Id);
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return this.Equals(obj as IChallenge);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

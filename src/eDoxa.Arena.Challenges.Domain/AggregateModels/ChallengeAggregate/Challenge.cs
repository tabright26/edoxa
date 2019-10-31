// Filename: Challenge.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Challenge : Entity<ChallengeId>, IChallenge
    {
        private readonly HashSet<Participant> _participants = new HashSet<Participant>();

        public Challenge(
            ChallengeName name,
            Game game,
            BestOf bestOf,
            Entries entries,
            ChallengeTimeline timeline,
            IScoring scoring
        )
        {
            Name = name;
            Game = game;
            BestOf = bestOf;
            Entries = entries;
            Timeline = timeline;
            Scoring = scoring;
        }

        public bool SoldOut => Participants.Count >= Entries;

        public ChallengeName Name { get; }

        public Game Game { get; }

        public ChallengeTimeline Timeline { get; private set; }

        public DateTime? SynchronizedAt { get; private set; }

        public BestOf BestOf { get; }

        public Entries Entries { get; }

        public IScoring Scoring { get; }

        public IScoreboard Scoreboard => new Scoreboard(this);

        public IReadOnlyCollection<Participant> Participants => _participants;

        public void Register(Participant participant)
        {
            if (!this.CanRegister(participant))
            {
                throw new InvalidOperationException();
            }

            _participants.Add(participant);
        }

        public void Start(IDateTimeProvider startedAt)
        {
            if (!this.CanStart())
            {
                throw new InvalidOperationException();
            }

            Timeline = Timeline.Start(startedAt);
        }

        public void Close(IDateTimeProvider closedAt)
        {
            if (!this.CanClose())
            {
                throw new InvalidOperationException();
            }

            Timeline = Timeline.Close(closedAt);
        }

        public void Synchronize(
            Func<GameAccountId, DateTime, DateTime, IEnumerable<GameReference>> getGameReferences,
            Func<GameAccountId, GameReference, IScoring, IMatch> getMatch,
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
                    Timeline.StartedAt ?? throw new InvalidOperationException(),
                    Timeline.EndedAt ?? throw new InvalidOperationException());

                foreach (var gameReference in participant.GetUnsynchronizedGameReferences(gameReferences))
                {
                    var match = getMatch(participant.GameAccountId, gameReference, Scoring);

                    participant.Snapshot(match);
                }
            }

            this.Synchronize(synchronizedAt);
        }

        public void Synchronize(IDateTimeProvider synchronizedAt)
        {
            SynchronizedAt = synchronizedAt.DateTime;
        }

        public bool ParticipantExists(UserId userId)
        {
            return Participants.Any(participant => participant.UserId == userId);
        }

        private bool CanRegister(Participant participant)
        {
            return !SoldOut && !this.ParticipantExists(participant.UserId);
        }

        private bool CanStart()
        {
            return Participants.Count == Entries;
        }

        private bool CanClose()
        {
            return Timeline == ChallengeState.Ended;
        }

        // TODO: Must be verified.
        private bool CanSynchronize()
        {
            return Timeline != ChallengeState.Inscription && Timeline != ChallengeState.Closed;
        }
    }

    public partial class Challenge : IEquatable<IChallenge?>
    {
        public bool Equals(IChallenge? challenge)
        {
            return Id.Equals(challenge?.Id);
        }

        public sealed override bool Equals(object? obj)
        {
            return this.Equals(obj as IChallenge);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

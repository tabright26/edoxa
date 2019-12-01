// Filename: Challenge.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Challenges.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public partial class Challenge : Entity<ChallengeId>, IChallenge
    {
        private readonly HashSet<Participant> _participants = new HashSet<Participant>();

        public Challenge(
            ChallengeId id,
            ChallengeName name,
            Game game,
            BestOf bestOf,
            Entries entries,
            ChallengeTimeline timeline,
            IScoring scoring
        )
        {
            this.SetEntityId(id);
            Name = name;
            Game = game;
            BestOf = bestOf;
            Entries = entries;
            Timeline = timeline;
            Scoring = scoring;
            this.AddDomainEvent(new ChallengeCreatedDomainEvent(this));
        }

        public bool SoldOut => _participants.Count >= Entries;

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

            this.AddDomainEvent(new ChallengeParticipantRegisteredDomainEvent(Id, participant.Id));
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

            this.AddDomainEvent(new ChallengeClosedDomainEvent(this));
        }

        public void Synchronize(IDateTimeProvider synchronizedAt)
        {
            if (!this.CanSynchronize())
            {
                throw new InvalidOperationException();
            }

            SynchronizedAt = synchronizedAt.DateTime;
        }

        public bool ParticipantExists(UserId userId)
        {
            return Participants.Any(participant => participant.UserId == userId);
        }

        public bool CanSynchronize(Participant participant)
        {
            return participant.SynchronizedAt < Timeline.EndedAt;
        }

        private bool CanRegister(Participant participant)
        {
            return !SoldOut && !this.ParticipantExists(participant.UserId);
        }

        private bool CanStart()
        {
            return Participants.Count == Entries;
        }

        public bool CanClose()
        {
            return Timeline == ChallengeState.Ended && Participants.All(participant => !this.CanSynchronize(participant));
        }

        public bool CanSynchronize()
        {
            return Timeline != ChallengeState.Inscription && Timeline != ChallengeState.Closed && Timeline.StartedAt.HasValue && Timeline.EndedAt.HasValue;
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

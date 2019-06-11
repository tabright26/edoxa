// Filename: Challenge.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Strategies;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.DomainEvents;
using eDoxa.Arena.Challenges.Domain.Validators;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Challenge : Entity<ChallengeId>, IChallenge, IAggregateRoot
    {
        private HashSet<Participant> _participants;
        private HashSet<ChallengeStat> _stats;
        private List<Bucket> _buckets;

        public Challenge(
            Game game,
            ChallengeName name,
            ChallengeSetup setup,
            ChallengeDuration duration
        ) : this()
        {
            Game = game;
            Name = name;
            Setup = setup;
            Timeline = new ChallengeTimeline(duration);
        }

        private Challenge()
        {
            TestMode = null;
            CreatedAt = DateTime.UtcNow;
            LastSync = null;
            _stats = new HashSet<ChallengeStat>();
            _participants = new HashSet<Participant>();
            _buckets = new List<Bucket>();
        }

        public TestMode TestMode { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? LastSync { get; private set; }

        public Game Game { get; private set; }

        public ChallengeName Name { get; private set; }

        public ChallengeSetup Setup { get; private set; }

        public ChallengeTimeline Timeline { get; private set; }

        public IReadOnlyCollection<ChallengeStat> Stats => _stats;

        public IReadOnlyCollection<Participant> Participants => _participants;

        public IReadOnlyCollection<Bucket> Buckets => _buckets;

        private IEnumerable<Participant> ParticipantsToSync => Participants.Where(participant => !participant.HasFinalScore).OrderBy(participant => participant.LastSync).ToList();

        public void ApplyScoringStrategy(IScoringStrategy strategy)
        {
            strategy.Scoring.ForEach(stat => _stats.Add(new ChallengeStat(stat.Key, stat.Value)));
        }

        public void ApplyPayoutStrategy(IPayoutStrategy strategy)
        {
            strategy.Payout.Buckets.ForEach(bucket => _buckets.Add(bucket));
        }

        public void EnableTestMode(TestMode testMode, ChallengeTimeline timeline)
        {
            TestMode = testMode;

            Timeline = timeline;
        }

        public async Task SynchronizeAsync(IMatchReferencesFactory matchReferencesFactory, IMatchStatsFactory matchStatsFactory)
        {
            foreach (var participant in ParticipantsToSync)
            {
                await this.SynchronizeAsync(matchReferencesFactory, matchStatsFactory, participant);

                participant.Sync();
            }

            LastSync = DateTime.UtcNow;
        }

        internal bool LastSyncMoreThan(TimeSpan timeSpan)
        {
            return LastSync.HasValue && LastSync.Value + timeSpan < DateTime.UtcNow;
        }

        private async Task SynchronizeAsync(IMatchReferencesFactory matchReferencesFactory, IMatchStatsFactory matchStatsFactory, Participant participant)
        {
            var adapter = await matchReferencesFactory.CreateAdapterAsync(Game, participant.ExternalAccount, Timeline);

            await this.SynchronizeAsync(adapter.MatchReferences, matchStatsFactory, participant);
        }

        private async Task SynchronizeAsync(IEnumerable<MatchReference> matchReferences, IMatchStatsFactory matchStatsFactory, Participant participant)
        {
            foreach (var matchReference in participant.GetUnsynchronizedMatchReferences(matchReferences))
            {
                await this.SnapshotParticipantMatchAsync(participant, matchReference, matchStatsFactory);
            }
        }

        private async Task SnapshotParticipantMatchAsync(Participant participant, MatchReference matchReference, IMatchStatsFactory factory)
        {
            var adapter = await factory.CreateAdapter(Game, participant.ExternalAccount, matchReference);

            this.SnapshotParticipantMatch(participant, matchReference, adapter.MatchStats);
        }

        internal void SnapshotParticipantMatch(Participant participant, MatchReference matchReference, IMatchStats matchStats)
        {
            var scoring = new Scoring(Stats);

            participant.SnapshotMatch(matchReference, matchStats, scoring);
        }

        public void TryClose(Action closeChallenge)
        {
            if (Timeline.State.Equals(ChallengeState.Ended))
            {
                Timeline = Timeline.Close();

                closeChallenge();
            }
        }

        public void DistributeParticipantPrizes()
        {
            var buckets = new Buckets(Buckets);

            var payout = new Payout(buckets);

            var scoreboard = new Scoreboard(Participants);

            this.AddDomainEvent(new ChallengePayoutDomainEvent(Id, payout.GetParticipantPrizes(scoreboard)));
        }

        public Participant RegisterParticipant(UserId userId, ExternalAccount externalAccount)
        {
            if (!this.CanRegisterParticipant(userId))
            {
                throw new InvalidOperationException();
            }

            var participant = new Participant(this, userId, externalAccount);

            _participants.Add(participant);

            this.TryStart();

            return participant;
        }

        private bool CanRegisterParticipant(UserId userId)
        {
            return new RegisterParticipantValidator(userId).Validate(this).IsValid;
        }

        internal bool CanStart()
        {
            return Participants.Count == Setup.Entries;
        }

        private void TryStart()
        {
            if (this.CanStart())
            {
                Timeline = Timeline.Start();
            }
        }
    }
}

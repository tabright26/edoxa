// Filename: FakeDefaultChallengeFactory.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Enumerations;

using Moq;

using Match = eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate.Match;

namespace eDoxa.Arena.Challenges.Tests.Factories
{
    public sealed partial class FakeChallengeFactory
    {
        public const string Kills = nameof(Kills);
        public const string Deaths = nameof(Deaths);
        public const string Assists = nameof(Assists);
        public const string TotalDamageDealtToChampions = nameof(TotalDamageDealtToChampions);
        public const string TotalHeal = nameof(TotalHeal);
        private static readonly Lazy<FakeChallengeFactory> Lazy = new Lazy<FakeChallengeFactory>(() => new FakeChallengeFactory());

        private static readonly MatchExternalId AdminMatchExternalId = new MatchExternalId(2973265231);
        private static readonly Random Random = new Random();

        public static FakeChallengeFactory Instance => Lazy.Value;

        public Challenge CreateChallenge(ChallengeState state = null, ChallengeSetup setup = null)
        {
            state = state ?? ChallengeState.Opened;

            setup = setup ?? new FakeChallengeSetup();

            var challenge = new Challenge(Game.LeagueOfLegends, new ChallengeName(nameof(Challenge)), setup);

            var timeline = this.CreateChallengeTimeline(state);

            challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(challenge, timeline);

            if (state.Value >= ChallengeState.Opened.Value)
            {
                var scoring = new Option<IScoring>(this.CreateScoring());

                challenge.GetType().GetField("_scoring", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(challenge, scoring);
            }

            return challenge;
        }

        public Challenge CreateChallengeWithParticipant(UserId userId)
        {
            var challenge = this.CreateChallenge();

            challenge.RegisterParticipant(userId, new ParticipantExternalAccount(Guid.NewGuid()));

            return challenge;
        }

        public Challenge CreateChallengeWithParticipants(int? participantCount = null)
        {
            var challenge = this.CreateChallenge();

            participantCount = participantCount ?? challenge.Setup.Entries;

            for (var index = 0; index < participantCount; index++)
            {
                challenge.RegisterParticipant(new UserId(), new ParticipantExternalAccount(Guid.NewGuid()));
            }

            return challenge;
        }
    }

    public sealed partial class FakeChallengeFactory
    {
        public ChallengeSetup CreateChallengeSetup(
            int bestOf = BestOf.Default,
            int entries = Entries.Default,
            decimal entryFee = EntryFee.Default,
            float payoutRatio = PayoutRatio.Default,
            float serviceChargeRatio = ServiceChargeRatio.Default)
        {
            return new ChallengeSetup(new BestOf(bestOf), new Entries(entries), new EntryFee(entryFee), new PayoutRatio(payoutRatio), new ServiceChargeRatio(serviceChargeRatio));
        }

        public Timeline CreateChallengeTimeline(ChallengeState state = null)
        {
            state = state ?? ChallengeState.Draft;

            if (state.Equals(ChallengeState.Draft))
            {
                return CreateChallengeTimelineAsDraft();
            }

            if (state.Equals(ChallengeState.Configured))
            {
                return CreateChallengeTimelineAsConfigured();
            }

            if (state.Equals(ChallengeState.Opened))
            {
                return CreateChallengeTimelineAsOpened();
            }

            if (state.Equals(ChallengeState.InProgress))
            {
                return CreateChallengeTimelineAsInProgress();
            }

            if (state.Equals(ChallengeState.Ended))
            {
                return CreateChallengeTimelineAsEnded();
            }

            if (state.Equals(ChallengeState.Closed))
            {
                return CreateChallengeTimelineAsClosed();
            }

            throw new ArgumentOutOfRangeException(nameof(state));
        }

        private static Timeline CreateChallengeTimelineAsDraft()
        {
            return new Timeline();
        }

        private static Timeline CreateChallengeTimelineAsConfigured()
        {
            var publishedAt = TimelinePublishedAt.Min.AddDays(1);

            var timeline = CreateChallengeTimelineAsDraft();

            timeline = timeline.Configure(publishedAt, TimelineRegistrationPeriod.Default, TimelineExtensionPeriod.Default);

            return timeline;
        }

        private static Timeline CreateChallengeTimelineAsOpened()
        {
            var timeline = CreateChallengeTimelineAsDraft();

            timeline = timeline.Publish(TimelineRegistrationPeriod.Default, TimelineExtensionPeriod.Default);

            return timeline;
        }

        private static Timeline CreateChallengeTimelineAsInProgress()
        {
            var timeline = CreateChallengeTimelineAsOpened();

            var publishedAt = timeline.PublishedAt - TimelineExtensionPeriod.Default;

            timeline.GetType().GetField("_publishedAt", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(timeline, publishedAt);

            return timeline;
        }

        private static Timeline CreateChallengeTimelineAsEnded()
        {
            var timeline = CreateChallengeTimelineAsInProgress();

            var publishedAt = timeline.PublishedAt - TimelineRegistrationPeriod.Default;

            timeline.GetType().GetField("_publishedAt", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(timeline, publishedAt);

            return timeline;
        }

        private static Timeline CreateChallengeTimelineAsClosed()
        {
            var timeline = CreateChallengeTimelineAsEnded();

            timeline = timeline.Close();

            return timeline;
        }
    }

    public sealed partial class FakeChallengeFactory
    {
        public Participant CreateParticipant(int? bestOf = null)
        {
            var setup = this.CreateChallengeSetup(bestOf ?? BestOf.DefaultValue);

            var challenge = this.CreateChallenge(setup: setup);

            return new Participant(challenge, new UserId(), new ParticipantExternalAccount(Guid.NewGuid()));
        }

        public Participant CreateParticipantMatches(int matchCount = 0, int? bestOf = null)
        {
            var participant = this.CreateParticipant(bestOf);

            for (var index = 0; index < matchCount; index++)
            {
                participant.SnapshotMatch(this.CreateMatchStats(), this.CreateScoring());
            }

            return participant;
        }

        public Match CreateMatch()
        {
            return new Match(this.CreateParticipant(), new MatchExternalId(AdminMatchExternalId.ToString()));
        }

        public IMatchStats CreateMatchStats(MatchExternalId matchExternalId = null)
        {
            matchExternalId = matchExternalId ?? new MatchExternalId(2233345251);

            return new MatchStats(
                matchExternalId,
                new
                {
                    Kills = Random.Next(0, 40 + 1),
                    Deaths = Random.Next(0, 15 + 1),
                    Assists = Random.Next(0, 50 + 1),
                    TotalDamageDealtToChampions = Random.Next(10000, 500000 + 1),
                    TotalHeal = Random.Next(10000, 350000 + 1)
                }
            );
        }

        public Stat CreateStat(StatName name, StatValue value, StatWeighting weighting)
        {
            return new Stat(new MatchId(), name, value, weighting);
        }

        public IScoringStrategy CreateScoringStrategy()
        {
            var mock = new Mock<IScoringStrategy>();

            mock.SetupGet(strategy => strategy.Scoring).Returns(this.CreateScoring());

            return mock.Object;
        }

        public IScoring CreateScoring()
        {
            return new Scoring
            {
                [Kills] = new StatWeighting(4F),
                [Deaths] = new StatWeighting(-3F),
                [Assists] = new StatWeighting(3F),
                [TotalDamageDealtToChampions] = new StatWeighting(0.00015F),
                [TotalHeal] = new StatWeighting(0.0008F)
            };
        }

        public Scoreboard CreateScoreboard()
        {
            return new Scoreboard(this.CreateChallenge(ChallengeState.Ended));
        }

        //public PrizePool CreatePrizePool(int prizePool)
        //{
        //    return new PrizePool(new Entries(payoutEntries * 2, false), new PayoutRatio(0.5F, false));
        //}

        //public PayoutEntries CreatePayoutEntries(int payoutEntries)
        //{
        //    return new PayoutEntries(new Entries(payoutEntries * 2, false), new PayoutRatio(0.5F, false));
        //}
    }
}
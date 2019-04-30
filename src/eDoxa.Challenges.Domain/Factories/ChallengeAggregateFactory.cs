// Filename: ChallengeAggregateFactory.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.Seedwork.Domain.Factories;

using Moq;

using Match = eDoxa.Challenges.Domain.AggregateModels.MatchAggregate.Match;

namespace eDoxa.Challenges.Domain.Factories
{
    internal sealed partial class ChallengeAggregateFactory : AggregateFactory
    {
        private static readonly Lazy<ChallengeAggregateFactory> Lazy = new Lazy<ChallengeAggregateFactory>(() => new ChallengeAggregateFactory());

        public static ChallengeAggregateFactory Instance => Lazy.Value;
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        internal const int DefaultRandomChallengeCount = 5;

        internal const string Kills = nameof(Kills);
        internal const string Deaths = nameof(Deaths);
        internal const string Assists = nameof(Assists);
        internal const string TotalDamageDealtToChampions = nameof(TotalDamageDealtToChampions);
        internal const string TotalHeal = nameof(TotalHeal);

        public static readonly LinkedMatch AdminLinkedMatch = new LinkedMatch(2973265231);

        private static readonly Random Random = new Random();

        public ChallengeName CreateChallengeName(string name = nameof(Challenge))
        {
            return new ChallengeName(name);
        }

        public LinkedAccount CreateLinkedAccount()
        {
            return new LinkedAccount(Guid.NewGuid());
        }

        public LinkedMatch CreateLinkedMatch()
        {
            return new LinkedMatch(Guid.NewGuid());
        }

        private static IEnumerable<ChallengeState1> OtherStates(ChallengeState1 state)
        {
            var states = Enum.GetValues(typeof(ChallengeState1)).Cast<ChallengeState1>().ToList();

            states.Remove(state);

            states.Remove(ChallengeState1.None);

            states.Remove(ChallengeState1.All);

            return states;
        }

        public BucketSizes CreateBucketSizes(int payoutEntries)
        {
            return new BucketSizes(this.CreatePayoutEntries(payoutEntries));
        }

        public BucketCount CreateBucketCount(int payoutEntries)
        {
            return new BucketCount(this.CreatePayoutEntries(payoutEntries));
        }

        public PayoutEntries CreatePayoutEntries(int payoutEntries)
        {
            return new PayoutEntries(new Entries(payoutEntries * 2, false), new PayoutRatio(0.5F, false));
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public Challenge CreateChallenge(Game game, ChallengeName name, ChallengeSetup setup)
        {
            return new Challenge(game, name, setup);
        }

        public Challenge CreateChallenge(ChallengeState1 state = ChallengeState1.Opened, ChallengeSetup setup = null)
        {
            setup = setup ?? new DefaultChallengeSetup();

            var challenge = this.CreateChallenge(Game.LeagueOfLegends, nameof(Challenge), setup);

            var timeline = this.CreateChallengeTimeline(state);

            challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(challenge, timeline);

            if (state >= ChallengeState1.Opened)
            {
                var scoring = new Option<IScoring>(this.CreateScoring());

                challenge.GetType().GetField("_scoring", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(challenge, scoring);
            }

            return challenge;
        }

        public Challenge CreateChallengeWithParticipant(UserId userId)
        {
            var challenge = this.CreateChallenge();

            var linkedAccount = this.CreateLinkedAccount();

            challenge.RegisterParticipant(userId, linkedAccount);

            return challenge;
        }

        public Challenge CreateChallengeWithParticipants(int? participantCount = null)
        {
            var challenge = this.CreateChallenge();

            participantCount = participantCount ?? challenge.Setup.Entries;

            for (var index = 0; index < participantCount; index++)
            {
                var linkedAccount = this.CreateLinkedAccount();

                challenge.RegisterParticipant(new UserId(), linkedAccount);
            }

            return challenge;
        }

        public Challenge CreateRandomChallenge(ChallengeState1 state = ChallengeState1.Opened)
        {
            return this.CreateRandomChallenges(state).First();
        }

        public IReadOnlyCollection<Challenge> CreateRandomChallengesWithOtherStates(ChallengeState1 state)
        {
            return this.CreateRandomChallenges(state).Union(this.CreateOtherRandomChallenges(state)).ToList();
        }

        public IReadOnlyCollection<Challenge> CreateRandomChallenges(ChallengeState1 state = ChallengeState1.Opened)
        {
            var challenges = new Collection<Challenge>();

            for (var row = 0; row < DefaultRandomChallengeCount; row++)
            {
                challenges.Add(this.CreateRandomChallengeFromState(state));
            }

            return challenges;
        }

        private IReadOnlyCollection<Challenge> CreateOtherRandomChallenges(ChallengeState1 state)
        {
            var challenges = new Collection<Challenge>();

            foreach (var otherState in OtherStates(state))
            {
                for (var row = 0; row < DefaultRandomChallengeCount; row++)
                {
                    challenges.Add(this.CreateRandomChallengeFromState(otherState));
                }
            }

            return challenges;
        }

        private Challenge CreateRandomChallengeFromState(ChallengeState1 state)
        {
            var challenge = this.CreateChallenge(state);

            if (state >= ChallengeState1.Opened)
            {
                var timeline = challenge.Timeline;

                challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(challenge, this.CreateChallengeTimeline(ChallengeState1.Opened));

                for (var row = 0; row < Random.Next(1, challenge.Setup.Entries + 1); row++)
                {
                    var userId = new UserId();

                    challenge.RegisterParticipant(userId, new LinkedAccount(Guid.NewGuid()));
                }

                challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(challenge, this.CreateChallengeTimeline(ChallengeState1.InProgress));

                foreach (var participant in challenge.Participants)
                {
                    for (var index = 0; index < Random.Next(1, challenge.Setup.BestOf + Random.Next(0, challenge.Setup.BestOf + 1) + 1); index++)
                    {
                        var stats = this.CreateMatchStats();

                        challenge.SnapshotParticipantMatch(participant.Id, stats);
                    }
                }

                challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(challenge, timeline);
            }

            return challenge;
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public ChallengeSetup CreateChallengeSetup(
            int bestOf = BestOf.Default,
            int entries = Entries.Default,
            decimal entryFee = EntryFee.Default,
            float payoutRatio = PayoutRatio.Default,
            float serviceChargeRatio = ServiceChargeRatio.Default)
        {
            return new ChallengeSetup(new BestOf(bestOf), new Entries(entries), new EntryFee(entryFee), new PayoutRatio(payoutRatio),
                new ServiceChargeRatio(serviceChargeRatio));
        }

        public ChallengeTimeline CreateChallengeTimeline(ChallengeState1 state = ChallengeState1.Draft)
        {
            switch (state)
            {
                case ChallengeState1.Draft:

                    return CreateChallengeTimelineAsDraft();
                case ChallengeState1.Configured:

                    return CreateChallengeTimelineAsConfigured();
                case ChallengeState1.Opened:

                    return CreateChallengeTimelineAsOpened();
                case ChallengeState1.InProgress:

                    return CreateChallengeTimelineAsInProgress();
                case ChallengeState1.Ended:

                    return CreateChallengeTimelineAsEnded();
                case ChallengeState1.Closed:

                    return CreateChallengeTimelineAsClosed();
                default:

                    throw new ArgumentOutOfRangeException(nameof(state));
            }
        }

        private static ChallengeTimeline CreateChallengeTimelineAsDraft()
        {
            return new ChallengeTimeline();
        }

        private static ChallengeTimeline CreateChallengeTimelineAsConfigured()
        {
            var publishedAt = TimelinePublishedAt.Min.AddDays(1);

            var timeline = CreateChallengeTimelineAsDraft();

            timeline = timeline.Configure(publishedAt, TimelineRegistrationPeriod.Default, TimelineExtensionPeriod.Default);

            return timeline;
        }

        private static ChallengeTimeline CreateChallengeTimelineAsOpened()
        {
            var timeline = CreateChallengeTimelineAsDraft();

            timeline = timeline.Publish(TimelineRegistrationPeriod.Default, TimelineExtensionPeriod.Default);

            return timeline;
        }

        private static ChallengeTimeline CreateChallengeTimelineAsInProgress()
        {
            var timeline = CreateChallengeTimelineAsOpened();

            var publishedAt = timeline.PublishedAt - TimelineExtensionPeriod.Default;

            timeline.GetType().GetField("_publishedAt", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(timeline, publishedAt);

            return timeline;
        }

        private static ChallengeTimeline CreateChallengeTimelineAsEnded()
        {
            var timeline = CreateChallengeTimelineAsInProgress();

            var publishedAt = timeline.PublishedAt - TimelineRegistrationPeriod.Default;

            timeline.GetType().GetField("_publishedAt", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(timeline, publishedAt);

            return timeline;
        }

        private static ChallengeTimeline CreateChallengeTimelineAsClosed()
        {
            var timeline = CreateChallengeTimelineAsEnded();

            timeline = timeline.Close();

            return timeline;
        }

        public IScoreboardStrategy CreateChallengeScoreboardStrategy()
        {
            var mock = new Mock<IScoreboardStrategy>();

            mock.SetupGet(strategy => strategy.Scoreboard).Returns(this.CreateChallengeScoreboard());

            return mock.Object;
        }

        public IScoreboard CreateChallengeScoreboard()
        {
            return new Scoreboard();
        }

        public ChallengeLiveData CreateChallengeLiveData(Challenge challenge)
        {
            return new ChallengeLiveData(challenge.Setup, challenge.Participants);
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public Participant CreateParticipant(Challenge challenge, UserId userId, LinkedAccount linkedAccount)
        {
            return new Participant(challenge, userId, linkedAccount);
        }

        public Participant CreateParticipant(int? bestOf = null)
        {
            var setup = this.CreateChallengeSetup(bestOf ?? BestOf.DefaultValue);

            var challenge = this.CreateChallenge(setup: setup);

            var linkedAccount = this.CreateLinkedAccount();

            return this.CreateParticipant(challenge, new UserId(), linkedAccount);
        }

        public Participant CreateParticipantWithMatches(int matchCount = 0, int? bestOf = null)
        {
            var participant = this.CreateParticipant(bestOf);

            for (var index = 0; index < matchCount; index++)
            {
                var stats = this.CreateMatchStats();

                var scoring = this.CreateScoring();

                participant.SnapshotMatch(stats, scoring);
            }

            return participant;
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public Match CreateMatch(Participant participant, LinkedMatch linkedMatch)
        {
            return new Match(participant, linkedMatch);
        }

        public Match CreateMatch()
        {
            var participant = this.CreateParticipant();

            var linkedMatch = this.CreateLinkedMatch();

            return this.CreateMatch(participant, linkedMatch);
        }

        public IMatchStats CreateMatchStats(LinkedMatch linkedMatch = null)
        {
            linkedMatch = linkedMatch ?? new LinkedMatch(2233345251);

            return new MatchStats(
                linkedMatch,
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

        public Stat CreateStat(MatchId matchId, StatName name, StatValue value, StatWeighting weighting)
        {
            return new Stat(matchId, name, value, weighting);
        }

        public Stat CreateStat(StatName name, StatValue value, StatWeighting weighting)
        {
            return this.CreateStat(new MatchId(), name, value, weighting);
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
    }
}
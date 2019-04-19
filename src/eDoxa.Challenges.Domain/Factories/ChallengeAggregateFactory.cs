// Filename: ChallengeAggregateFactory.cs
// Date Created: 2019-04-14
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

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.Seedwork.Domain.Factories;

using Moq;

using Match = eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Match;

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

        public static readonly LinkedMatch AdminLinkedMatch = LinkedMatch.FromLong(2973265231);

        private static readonly Random Random = new Random();

        public ChallengeName CreateChallengeName(string name = nameof(Challenge))
        {
            return new ChallengeName(name);
        }

        public LinkedAccount CreateLinkedAccount()
        {
            return LinkedAccount.FromGuid(Guid.NewGuid());
        }

        public LinkedMatch CreateLinkedMatch()
        {
            return LinkedMatch.FromGuid(Guid.NewGuid());
        }

        private static IEnumerable<ChallengeState> OtherStates(ChallengeState state)
        {
            var states = Enum.GetValues(typeof(ChallengeState)).Cast<ChallengeState>().ToList();

            states.Remove(state);

            states.Remove(ChallengeState.None);

            states.Remove(ChallengeState.All);

            return states;
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public Challenge CreateChallenge(Game game, ChallengeName name, ChallengeSettings settings)
        {
            return new Challenge(game, name, settings);
        }

        public Challenge CreateChallenge(ChallengeState state = ChallengeState.Opened, ChallengeSettings settings = null)
        {
            settings = settings ?? new ChallengeSettings();

            var challenge = this.CreateChallenge(Game.LeagueOfLegends, nameof(Challenge), settings);

            var timeline = this.CreateChallengeTimeline(state);

            challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(challenge, timeline);

            if (state >= ChallengeState.Opened)
            {
                var scoring = this.CreateChallengeScoring();

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

            participantCount = participantCount ?? challenge.Settings.Entries.ToInt32();

            for (var index = 0; index < participantCount; index++)
            {
                var linkedAccount = this.CreateLinkedAccount();

                challenge.RegisterParticipant(new UserId(), linkedAccount);
            }

            return challenge;
        }

        public Challenge CreateRandomChallenge(ChallengeState state = ChallengeState.Opened)
        {
            return this.CreateRandomChallenges(state).First();
        }

        public IReadOnlyCollection<Challenge> CreateRandomChallengesWithOtherStates(ChallengeState state)
        {
            return this.CreateRandomChallenges(state).Union(this.CreateOtherRandomChallenges(state)).ToList();
        }

        public IReadOnlyCollection<Challenge> CreateRandomChallenges(ChallengeState state = ChallengeState.Opened)
        {
            var challenges = new Collection<Challenge>();

            for (var row = 0; row < DefaultRandomChallengeCount; row++)
            {
                challenges.Add(this.CreateRandomChallengeFromState(state));
            }

            return challenges;
        }

        private IReadOnlyCollection<Challenge> CreateOtherRandomChallenges(ChallengeState state)
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

        private Challenge CreateRandomChallengeFromState(ChallengeState state)
        {
            var challenge = this.CreateChallenge(state);

            if (state >= ChallengeState.Opened)
            {
                var timeline = challenge.Timeline;

                challenge.GetType().GetField("_timeline", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(challenge, this.CreateChallengeTimeline(ChallengeState.Opened));

                for (var row = 0; row < Random.Next(1, challenge.Settings.Entries.ToInt32() + 1); row++)
                {
                    var participant = challenge.RegisterParticipant(new UserId(), LinkedAccount.FromGuid(Guid.NewGuid()));

                    for (var index = 0;
                        index < Random.Next(1, challenge.Settings.BestOf.ToInt32() + Random.Next(0, challenge.Settings.BestOf.ToInt32() + 1) + 1);
                        index++)
                    {
                        var stats = this.CreateChallengeStats();

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
        public ChallengeSettings CreateChallengeSettings(
            int bestOf = BestOf.DefaultPrimitive,
            int entries = Entries.DefaultPrimitive,
            decimal entryFee = EntryFee.DefaultPrimitive,
            float payoutRatio = PayoutRatio.DefaultPrimitive,
            float serviceChargeRatio = ServiceChargeRatio.DefaultPrimitive)
        {
            return new ChallengeSettings(bestOf, entries, entryFee, payoutRatio, serviceChargeRatio);
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public ChallengeTimeline CreateChallengeTimeline(ChallengeState state = ChallengeState.Draft)
        {
            switch (state)
            {
                case ChallengeState.Draft:

                    return CreateChallengeTimelineAsDraft();
                case ChallengeState.Configured:

                    return CreateChallengeTimelineAsConfigured();
                case ChallengeState.Opened:

                    return CreateChallengeTimelineAsOpened();
                case ChallengeState.InProgress:

                    return CreateChallengeTimelineAsInProgress();
                case ChallengeState.Ended:

                    return CreateChallengeTimelineAsEnded();
                case ChallengeState.Closed:

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
            var publishedAt = ChallengeTimeline.MinPublishedAt.AddDays(1);

            var timeline = CreateChallengeTimelineAsDraft();

            timeline = timeline.Configure(publishedAt, ChallengeTimeline.DefaultRegistrationPeriod, ChallengeTimeline.DefaultExtensionPeriod);

            return timeline;
        }

        private static ChallengeTimeline CreateChallengeTimelineAsOpened()
        {
            var timeline = CreateChallengeTimelineAsDraft();

            timeline = timeline.Publish(ChallengeTimeline.DefaultRegistrationPeriod, ChallengeTimeline.DefaultExtensionPeriod);

            return timeline;
        }

        private static ChallengeTimeline CreateChallengeTimelineAsInProgress()
        {
            var timeline = CreateChallengeTimelineAsOpened();

            var publishedAt = timeline.PublishedAt - ChallengeTimeline.DefaultExtensionPeriod;

            timeline.GetType().GetField("_publishedAt", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(timeline, publishedAt);

            return timeline;
        }

        private static ChallengeTimeline CreateChallengeTimelineAsEnded()
        {
            var timeline = CreateChallengeTimelineAsInProgress();

            var publishedAt = timeline.PublishedAt - ChallengeTimeline.DefaultRegistrationPeriod;

            timeline.GetType().GetField("_publishedAt", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(timeline, publishedAt);

            return timeline;
        }

        private static ChallengeTimeline CreateChallengeTimelineAsClosed()
        {
            var timeline = CreateChallengeTimelineAsEnded();

            timeline = timeline.Close();

            return timeline;
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public ChallengeLiveData CreateChallengeLive(Challenge challenge)
        {
            return new ChallengeLiveData(challenge);
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
            var settings = this.CreateChallengeSettings(bestOf ?? BestOf.Default.ToInt32());

            var challenge = this.CreateChallenge(settings: settings);

            var linkedAccount = this.CreateLinkedAccount();

            return this.CreateParticipant(challenge, new UserId(), linkedAccount);
        }

        public Participant CreateParticipantWithMatches(int matchCount = 0, int? bestOf = null)
        {
            var participant = this.CreateParticipant(bestOf);

            for (var index = 0; index < matchCount; index++)
            {
                var stats = this.CreateChallengeStats();

                var scoring = this.CreateChallengeScoring();

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
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public Stat CreateStat(MatchId matchId, string name, double value, float weighting)
        {
            return new Stat(matchId, name, value, weighting);
        }

        public Stat CreateStat(string name, double value, float weighting)
        {
            return this.CreateStat(new MatchId(), name, value, weighting);
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public IChallengeScoringStrategy CreateChallengeScoringStrategy()
        {
            var mock = new Mock<IChallengeScoringStrategy>();

            mock.SetupGet(strategy => strategy.Scoring).Returns(this.CreateChallengeScoring());

            return mock.Object;
        }

        public IChallengeScoring CreateChallengeScoring()
        {
            return new ChallengeScoring
            {
                [Kills] = 4F,
                [Deaths] = -3F,
                [Assists] = 3F,
                [TotalDamageDealtToChampions] = 0.00015F,
                [TotalHeal] = 0.0008F
            };
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public IChallengeScoreboardStrategy CreateChallengeScoreboardStrategy()
        {
            var mock = new Mock<IChallengeScoreboardStrategy>();

            mock.SetupGet(strategy => strategy.Scoreboard).Returns(this.CreateChallengeScoreboard());

            return mock.Object;
        }

        public IChallengeScoreboard CreateChallengeScoreboard()
        {
            return new ChallengeScoreboard();
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public IChallengePayoutStrategy CreateChallengePayoutStrategy()
        {
            var mock = new Mock<IChallengePayoutStrategy>();

            mock.SetupGet(strategy => strategy.Payout).Returns(this.CreateChallengePayout());

            return mock.Object;
        }

        public IChallengePayout CreateChallengePayout()
        {
            return new ChallengePayout();
        }
    }

    internal sealed partial class ChallengeAggregateFactory
    {
        public IChallengeStatsAdapter CreateChallengeStatsAdapter()
        {
            var mock = new Mock<IChallengeStatsAdapter>();

            mock.SetupGet(strategy => strategy.Stats).Returns(this.CreateChallengeStats());

            return mock.Object;
        }

        public IChallengeStats CreateChallengeStats(LinkedMatch linkedMatch = null)
        {
            linkedMatch = linkedMatch ?? LinkedMatch.FromLong(2233345251);

            return new ChallengeStats(
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
    }
}
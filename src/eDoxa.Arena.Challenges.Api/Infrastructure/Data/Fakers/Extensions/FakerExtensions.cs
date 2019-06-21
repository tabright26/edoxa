// Filename: FakerExtensions.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Domain.Aggregate;

using Moq;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions
{
    public static class FakerExtensions
    {
        public static ChallengeGame ChallengeGame(this Faker faker, ChallengeGame game = null)
        {
            return game ?? faker.PickRandom(Domain.AggregateModels.ChallengeAggregate.ChallengeGame.LeagueOfLegends);
        }

        public static ChallengeId ChallengeId(this Faker faker)
        {
            return Domain.AggregateModels.ChallengeAggregate.ChallengeId.FromGuid(faker.Random.Guid());
        }

        public static ChallengeName ChallengeName(this Faker faker)
        {
            return faker.PickRandom(new ChallengeName(nameof(Challenge)));
        }

        public static ChallengeSetup ChallengeSetup(this Faker faker, CurrencyType entryFeeCurrency = null)
        {
            var bestOf = faker.PickRandom(ValueObject.GetAllowValues<BestOf>());

            var payoutEntries = faker.PickRandom(ValueObject.GetAllowValues<PayoutEntries>());

            var moneyEntryFees = ValueObject.GetAllowValues<MoneyEntryFee>();

            var tokenEntryFees = ValueObject.GetAllowValues<TokenEntryFee>();

            if (entryFeeCurrency != null)
            {
                if (entryFeeCurrency == CurrencyType.Money)
                {
                    return new ChallengeSetup(bestOf, payoutEntries, faker.PickRandom(moneyEntryFees));
                }

                if (entryFeeCurrency == CurrencyType.Token)
                {
                    return new ChallengeSetup(bestOf, payoutEntries, faker.PickRandom(tokenEntryFees));
                }
            }

            return new ChallengeSetup(bestOf, payoutEntries, faker.PickRandom(moneyEntryFees.Cast<EntryFee>().Union(tokenEntryFees)));
        }

        public static ChallengeTimeline ChallengeTimeline(this Faker faker, ChallengeState state = null)
        {
            state = faker.ChallengeState(state);

            var duration = faker.ChallengeDuration();

            if (state == Domain.AggregateModels.ChallengeAggregate.ChallengeState.Closed)
            {
                return new ChallengeTimeline(duration, DateTime.UtcNow.DateKeepHours().Subtract(duration), DateTime.UtcNow.DateKeepHours());
            }

            if (state == Domain.AggregateModels.ChallengeAggregate.ChallengeState.Ended)
            {
                return new ChallengeTimeline(duration, DateTime.UtcNow.DateKeepHours().Subtract(duration));
            }

            if (state == Domain.AggregateModels.ChallengeAggregate.ChallengeState.InProgress)
            {
                return new ChallengeTimeline(duration, DateTime.UtcNow.DateKeepHours());
            }

            return new ChallengeTimeline(duration);
        }

        public static ChallengeDuration ChallengeDuration(this Faker faker)
        {
            return faker.PickRandom(ValueObject.GetAllowValues<ChallengeDuration>());
        }

        public static ChallengeState ChallengeState(this Faker faker, ChallengeState state = null)
        {
            return state ?? faker.PickRandom(Enumeration<ChallengeState>.GetEnumerations(true));
        }

        public static DateTime ChallengeCreatedAt(this Faker faker, ChallengeModel challenge)
        {
            return faker.Date.Recent(
                1,
                challenge.Timeline.StartedAt.HasValue ? challenge.Participants.Min(participant => participant.RegisteredAt) : DateTime.UtcNow.DateKeepHours()
            );
        }

        // TODO: To refactor.
        public static Entries ChallegeSetupEntries(this Faker faker, ChallengeModel challenge)
        {
            var timeline = new ChallengeTimeline(
                new ChallengeDuration(TimeSpan.FromTicks(challenge.Timeline.Duration)),
                challenge.Timeline.StartedAt,
                challenge.Timeline.ClosedAt
            );

            return timeline.State == Domain.AggregateModels.ChallengeAggregate.ChallengeState.Inscription
                ? new Entries(faker.Random.Int(1, challenge.Setup.Entries - 1))
                : new Entries(challenge.Setup.Entries);
        }

        public static ParticipantId ParticipantId(this Faker faker)
        {
            return Domain.AggregateModels.ChallengeAggregate.ParticipantId.FromGuid(faker.Random.Guid());
        }

        public static DateTime ParticipantTimestamp(this Faker faker, ChallengeModel challengeModel)
        {
            var timeline = new ChallengeTimeline(
                new ChallengeDuration(TimeSpan.FromTicks(challengeModel.Timeline.Duration)),
                challengeModel.Timeline.StartedAt,
                challengeModel.Timeline.ClosedAt
            );

            if (timeline.State != Domain.AggregateModels.ChallengeAggregate.ChallengeState.Inscription)
            {
                return faker.Date.Recent(1, timeline.StartedAt);
            }

            return faker.Date.Soon(1, DateTime.UtcNow.DateKeepHours());
        }

        public static MatchId MatchId(this Faker faker)
        {
            return Domain.AggregateModels.ChallengeAggregate.MatchId.FromGuid(faker.Random.Guid());
        }

        public static GameMatchId MatchReference(this Faker faker, ChallengeGame game)
        {
            if (game == Domain.AggregateModels.ChallengeAggregate.ChallengeGame.LeagueOfLegends)
            {
                return new GameMatchId(faker.Random.Long(1000000000, 9999999999));
            }

            throw new ArgumentNullException(nameof(game));
        }

        public static DateTime MatchTimestamp(this Faker faker, ChallengeTimelineModel timeline)
        {
            return faker.Date.Soon(1, timeline.StartedAt);
        }

        public static IMatchStats MatchStats(this Faker faker, ChallengeGame game)
        {
            if (game == Domain.AggregateModels.ChallengeAggregate.ChallengeGame.LeagueOfLegends)
            {
                return new MatchStats(
                    new
                    {
                        Kills = faker.Random.Int(0, 40),
                        Deaths = faker.Random.Int(0, 15),
                        Assists = faker.Random.Int(0, 50),
                        TotalDamageDealtToChampions = faker.Random.Int(10000, 500000),
                        TotalHeal = faker.Random.Int(10000, 350000)
                    }
                );
            }

            throw new ArgumentNullException(nameof(game));
        }

        public static GameAccountId UserGameReference(this Faker faker, ChallengeGame game)
        {
            if (game == Domain.AggregateModels.ChallengeAggregate.ChallengeGame.LeagueOfLegends)
            {
                return new GameAccountId(faker.Random.Replace("*****_*************************"));
            }

            throw new ArgumentNullException(nameof(game));
        }

        public static IMatchStatsAdapter MatchStatsAdapter(this Faker faker, ChallengeGame game)
        {
            var mock = new Mock<IMatchStatsAdapter>();

            var matchStats = faker.MatchStats(game);

            mock.SetupGet(adapter => adapter.MatchStats).Returns(matchStats);

            return mock.Object;
        }
    }
}

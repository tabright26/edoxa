// Filename: ChallengeFakerExtensions.cs
// Date Created: 2019-06-13
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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ValueObjects;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers.Extensions
{
    public static class ChallengeFakerExtensions
    {
        public static Game ChallengeGame(this Faker faker, Game game = null)
        {
            return game ?? faker.PickRandom(Game.GetAll());
        }

        public static ChallengeId ChallengeId(this Faker faker)
        {
            return AggregateModels.ChallengeAggregate.ChallengeId.FromGuid(faker.Random.Guid());
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

        public static ChallengeTimeline ChallengeTimeline(this Faker faker, ChallengeDuration duration = null, ChallengeState state = null)
        {
            duration = duration ?? faker.ChallengeDuration();

            state = state ?? faker.ChallengeState();

            if (state == AggregateModels.ChallengeAggregate.ChallengeState.Closed)
            {
                return new ChallengeTimeline(duration, DateTime.UtcNow.DateKeepHours().Subtract(duration), DateTime.UtcNow.DateKeepHours());
            }

            if (state == AggregateModels.ChallengeAggregate.ChallengeState.Ended)
            {
                return new ChallengeTimeline(duration, DateTime.UtcNow.DateKeepHours().Subtract(duration));
            }

            if (state == AggregateModels.ChallengeAggregate.ChallengeState.InProgress)
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
            return state ?? faker.PickRandom(Enumeration<ChallengeState>.GetAll());
        }

        public static DateTime ChallengeCreatedAt(this Faker faker, ChallengeTimeline timeline)
        {
            return faker.Date.Recent(2, timeline.StartedAt ?? DateTime.UtcNow.DateKeepHours());
        }

        public static Entries ChallegeSetupEntries(this Faker faker, Challenge challenge)
        {
            return challenge.State == AggregateModels.ChallengeAggregate.ChallengeState.Inscription
                ? new Entries(faker.Random.Int(1, challenge.Setup.Entries - 1))
                : challenge.Setup.Entries;
        }
    }
}

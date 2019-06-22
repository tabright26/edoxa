// Filename: ChallengeDataSet.cs
// Date Created: 2019-06-22
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

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Fakers.DataSets
{
    public class ChallengeDataSet
    {
        public ChallengeDataSet(Faker faker)
        {
            Faker = faker;
        }

        public Faker Faker { get; }

        public ChallengeId Id()
        {
            return ChallengeId.FromGuid(Faker.Random.Guid());
        }

        public ChallengeName Name()
        {
            return new ChallengeName(Faker.PickRandom("Daily Challenge", "Monthly Challenge", "Weekly Challenge"));
        }

        public ChallengeGame Game(ChallengeGame game = null)
        {
            return game ?? Faker.PickRandom(ChallengeGame.GetEnumerations());
        }

        public DateTime CreatedAt(DateTime? startedAt, IEnumerable<DateTime> participantRegistrations)
        {
            return Faker.Date.Recent(1, startedAt.HasValue ? participantRegistrations.Min() : DateTime.UtcNow.DateKeepHours());
        }

        public ChallengeState State(ChallengeState state = null)
        {
            return state ?? Faker.PickRandom(ChallengeState.GetEnumerations());
        }

        public ChallengeTimeline Timeline(ChallengeState state = null)
        {
            state = Faker.Challenge().State(state);

            var duration = Faker.Timeline().Duration();

            if (state == ChallengeState.Closed)
            {
                return new ChallengeTimeline(duration, DateTime.UtcNow.DateKeepHours().Subtract(duration), DateTime.UtcNow.DateKeepHours());
            }

            if (state == ChallengeState.Ended)
            {
                return new ChallengeTimeline(duration, DateTime.UtcNow.DateKeepHours().Subtract(duration));
            }

            if (state == ChallengeState.InProgress)
            {
                return new ChallengeTimeline(duration, DateTime.UtcNow.DateKeepHours());
            }

            return new ChallengeTimeline(duration);
        }

        public ChallengeSetup Setup(CurrencyType entryFeeCurrency = null)
        {
            return new ChallengeSetup(Faker.Setup().BestOf(), Faker.Setup().PayoutEntries(), Faker.Setup().EntryFee(entryFeeCurrency));
        }

        public IScoring Scoring(ChallengeGame game)
        {
            return ScoringFactory.Instance.CreateStrategy(this.Game(game)).Scoring;
        }

        public IPayout Payout(PayoutEntries payoutEntries, EntryFee entryFee)
        {
            return PayoutFactory.Instance.CreateStrategy(payoutEntries, entryFee).Payout;
        }

        public IEnumerable<ParticipantModel> Participants(
            ChallengeModel challengeModel,
            ChallengeGame game,
            ChallengeState state,
            BestOf bestOf,
            Entries entries,
            DateTime? startedAt
        )
        {
            for (var index = 0; index < this.ParticipantCount(state, entries); index++)
            {
                yield return this.Participant(
                    challengeModel,
                    game,
                    state,
                    bestOf,
                    startedAt
                );
            }
        }

        private Entries ParticipantCount(ChallengeState state, Entries entries)
        {
            return state == ChallengeState.Inscription ? new Entries(Faker.Random.Int(1, entries - 1)) : entries;
        }

        public ParticipantModel Participant(
            ChallengeModel challengeModel,
            ChallengeGame game,
            ChallengeState state,
            BestOf bestOf,
            DateTime? startedAt
        )
        {
            var participant = new ParticipantModel
            {
                Id = Faker.Participant().Id(),
                UserId = Faker.UserId(),
                GameAccountId = Faker.Participant().GameAccountId(game),
                Challenge = challengeModel
            };

            participant.Matches = Faker.Participant()
                .Matches(
                    participant,
                    game,
                    state,
                    bestOf,
                    startedAt
                )
                .ToList();

            participant.RegisteredAt = Faker.Participant().RegisteredAt(state, startedAt);

            participant.SynchronizedAt = participant.Matches.Max(match => match.SynchronizedAt as DateTime?);

            return participant;
        }
    }
}

// Filename: ChallengeFaker.cs
// Date Created: 2019-06-21
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Fakers
{
    public sealed class ChallengeFaker : Faker<ChallengeModel>
    {
        public ChallengeFaker(ChallengeGame game = null, ChallengeState state = null, CurrencyType entryFeeCurrency = null)
        {
            this.UseSeed(8675309);

            this.CustomInstantiator(
                faker =>
                {
                    game = faker.Challenge().Game(game);
                    var timeline = faker.Challenge().Timeline(state);
                    var setup = faker.Challenge().Setup(entryFeeCurrency);

                    var challengeModel = new ChallengeModel
                    {
                        Id = faker.Challenge().Id(),
                        Name = faker.Challenge().Name(),
                        Game = game.Value,
                        Timeline = new ChallengeTimelineModel
                        {
                            Duration = timeline.Duration.Ticks,
                            StartedAt = timeline.StartedAt,
                            ClosedAt = timeline.ClosedAt
                        },
                        Setup = new ChallengeSetupModel
                        {
                            BestOf = setup.BestOf,
                            Entries = setup.Entries,
                            PayoutEntries = setup.PayoutEntries,
                            EntryFeeCurrency = setup.EntryFee.Type.Value,
                            EntryFeeAmount = setup.EntryFee.Amount
                        },
                        Seed = localSeed
                    };

                    challengeModel.Buckets = this.Buckets(setup.PayoutEntries, setup.EntryFee).ToList();

                    challengeModel.ScoringItems = this.ScoringItems(game).ToList();

                    challengeModel.Participants = faker.Challenge()
                        .Participants(
                            challengeModel,
                            game,
                            timeline.State,
                            setup.BestOf,
                            setup.Entries,
                            timeline.StartedAt
                        )
                        .ToList();

                    challengeModel.CreatedAt = faker.Challenge()
                        .CreatedAt(timeline.StartedAt, challengeModel.Participants.Select(participant => participant.RegisteredAt));

                    challengeModel.SynchronizedAt = challengeModel.Participants.SelectMany(participant => participant.Matches)
                        .Max(participant => participant.SynchronizedAt as DateTime?);

                    return challengeModel;
                }
            );
        }

        private IEnumerable<ScoringItemModel> ScoringItems(ChallengeGame game)
        {
            return FakerHub.Challenge()
                .Scoring(game)
                .Select(
                    item => new ScoringItemModel
                    {
                        Name = item.Key,
                        Weighting = item.Value
                    }
                );
        }

        private IEnumerable<BucketModel> Buckets(PayoutEntries payoutEntries, EntryFee entryFee)
        {
            return FakerHub.Challenge()
                .Payout(payoutEntries, entryFee)
                .Buckets.Select(
                    bucket => new BucketModel
                    {
                        Size = bucket.Size,
                        PrizeCurrency = bucket.Prize.Type.Value,
                        PrizeAmount = bucket.Prize.Amount
                    }
                );
        }
    }
}

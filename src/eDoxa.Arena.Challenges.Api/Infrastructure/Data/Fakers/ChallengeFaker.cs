// Filename: ChallengeTestFaker.cs
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

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers
{
    public sealed class ChallengeFaker : Faker<ChallengeModel>
    {
        public ChallengeFaker(ChallengeGame game = null, ChallengeState state = null, CurrencyType entryFeeCurrency = null)
        {
            this.UseSeed(8675309);

            this.CustomInstantiator(
                faker =>
                {
                    var challengeTimelineModel = faker.ChallengeTimeline(state);

                    var challengeSetupModel = faker.ChallengeSetup(entryFeeCurrency);

                    var challengeModel = new ChallengeModel
                    {
                        Id = faker.ChallengeId(),
                        Name = faker.ChallengeName(),
                        Game = faker.ChallengeGame(game).Value,
                        Timeline = new ChallengeTimelineModel
                        {
                            Duration = challengeTimelineModel.Duration.Ticks,
                            StartedAt = challengeTimelineModel.StartedAt,
                            ClosedAt = challengeTimelineModel.ClosedAt
                        },
                        Setup = new ChallengeSetupModel
                        {
                            BestOf = challengeSetupModel.BestOf,
                            Entries = challengeSetupModel.Entries,
                            PayoutEntries = challengeSetupModel.PayoutEntries,
                            EntryFeeCurrency = challengeSetupModel.EntryFee.Type.Value,
                            EntryFeeAmount = challengeSetupModel.EntryFee.Amount
                        },
                        Seed = localSeed
                    };

                    challengeModel.Buckets = Buckets(
                            challengeSetupModel.PayoutEntries,
                            challengeSetupModel.EntryFee.Type.Value,
                            challengeSetupModel.EntryFee.Amount
                        )
                        .ToList();

                    challengeModel.ScoringItems = ScoringItemModels(challengeModel.Game).ToList();

                    challengeModel.Participants = this.Participants(challengeModel).ToList();

                    challengeModel.CreatedAt = faker.ChallengeCreatedAt(challengeModel);

                    challengeModel.SynchronizedAt = challengeModel.Participants.SelectMany(participant => participant.Matches)
                        .Max(participant => participant.SynchronizedAt as DateTime?);

                    return challengeModel;
                }
            );
        }

        private static IEnumerable<ScoringItemModel> ScoringItemModels(int game)
        {
            return ScoringFactory.Instance.CreateStrategy(ChallengeGame.FromValue(game))
                .Scoring.Select(
                    item => new ScoringItemModel
                    {
                        Name = item.Key,
                        Weighting = item.Value
                    }
                );
        }

        private static IEnumerable<BucketModel> Buckets(int payoutEntries, int entryFeeCurrency, decimal entryFeeAmount)
        {
            return PayoutFactory.Instance
                .CreateStrategy(new PayoutEntries(payoutEntries), new EntryFee(CurrencyType.FromValue(entryFeeCurrency), entryFeeAmount))
                .Payout.Buckets.Select(
                    bucket => new BucketModel
                    {
                        Size = bucket.Size,
                        PrizeCurrency = bucket.Prize.Type.Value,
                        PrizeAmount = bucket.Prize.Amount
                    }
                );
        }

        private IEnumerable<ParticipantModel> Participants(ChallengeModel challengeModel)
        {
            for (var index = 0; index < FakerHub.ChallegeSetupEntries(challengeModel); index++)
            {
                yield return this.Participant(challengeModel);
            }
        }

        private ParticipantModel Participant(ChallengeModel challengeModel)
        {
            var participant = new ParticipantModel
            {
                Id = FakerHub.ParticipantId(),
                UserId = FakerHub.UserId(),
                GameAccountId = FakerHub.GameMatchId(ChallengeGame.FromValue(challengeModel.Game)),
                Challenge = challengeModel
            };

            participant.Matches = this.Matches(participant).ToList();

            participant.RegisteredAt = FakerHub.ParticipantTimestamp(challengeModel);

            participant.SynchronizedAt = participant.Matches.Max(match => match.SynchronizedAt as DateTime?);

            return participant;
        }

        private IEnumerable<MatchModel> Matches(ParticipantModel participantModel)
        {
            for (var index = 0; index < this.MatchCount(participantModel.Challenge); index++)
            {
                yield return this.Match(participantModel, participantModel.Challenge.Game);
            }
        }

        private int MatchCount(ChallengeModel challengeModel)
        {
            return ChallengeState.Resolve(
                       TimeSpan.FromTicks(challengeModel.Timeline.Duration),
                       challengeModel.Timeline.StartedAt,
                       challengeModel.Timeline.ClosedAt
                   ) !=
                   ChallengeState.Inscription
                ? FakerHub.Random.Int(1, challengeModel.Setup.BestOf + 3)
                : 0;
        }

        private MatchModel Match(ParticipantModel participantModel, int game)
        {
            return new MatchModel
            {
                Id = FakerHub.MatchId(),
                GameMatchId = FakerHub.GameMatchId(ChallengeGame.FromValue(game)),
                SynchronizedAt = FakerHub.MatchTimestamp(participantModel.Challenge.Timeline),
                Stats = this.Stats(participantModel.Challenge.ScoringItems, game).ToList(),
                Participant = participantModel
            };
        }

        private IEnumerable<StatModel> Stats(ICollection<ScoringItemModel> scoringItemModels, int game)
        {
            var stats = FakerHub.MatchStats(ChallengeGame.FromValue(game));

            for (var index = 0; index < scoringItemModels.Count; index++)
            {
                var item = scoringItemModels.ElementAt(index);

                var name = item.Name;

                if (!stats.ContainsKey(new StatName(name)))
                {
                    continue;
                }

                var value = stats[new StatName(name)];

                var weighting = item.Weighting;

                yield return new StatModel
                {
                    Name = name,
                    Value = value,
                    Weighting = weighting
                };
            }
        }
    }
}

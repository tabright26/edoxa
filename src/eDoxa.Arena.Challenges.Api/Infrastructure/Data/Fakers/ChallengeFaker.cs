// Filename: ChallengeFaker.cs
// Date Created: 2019-06-20
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

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers
{
    public class ChallengeFaker : Faker<ChallengeModel>
    {
        private ParticipantFaker _participantFaker;

        public ChallengeFaker(ChallengeGame game = null, ChallengeState state = null, CurrencyType entryFeeCurrency = null)
        {
            this.UseSeed(8675309);

            this.RuleFor(challenge => challenge.Id, faker => faker.ChallengeId());

            this.RuleFor(challenge => challenge.Name, faker => faker.ChallengeName().ToString());

            this.RuleFor(challenge => challenge.Game, faker => faker.ChallengeGame(game).Value);

            this.RuleFor(
                challenge => challenge.Timeline,
                faker =>
                {
                    var timeline = faker.ChallengeTimeline(state);

                    return new ChallengeTimelineModel
                    {
                        Duration = timeline.Duration.Ticks,
                        StartedAt = timeline.StartedAt,
                        ClosedAt = timeline.ClosedAt
                    };
                }
            );

            this.RuleFor(
                challenge => challenge.Setup,
                faker =>
                {
                    var setup = faker.ChallengeSetup(entryFeeCurrency);

                    return new ChallengeSetupModel
                    {
                        Entries = setup.Entries,
                        PayoutEntries = setup.PayoutEntries,
                        EntryFeeCurrency = setup.EntryFee.Type.Value,
                        EntryFeeAmount = setup.EntryFee.Amount,
                        BestOf = setup.BestOf
                    };
                }
            );

            this.RuleFor(
                challenge => challenge.ScoringItems,
                (faker, challenge) =>
                {
                    var scoring = ScoringFactory.Instance.CreateStrategy(ChallengeGame.FromValue(challenge.Game)).Scoring;

                    return scoring.Select(
                            sc => new ScoringItemModel
                            {
                                Name = sc.Key.ToString(),
                                Weighting = sc.Value
                            }
                        )
                        .ToList();
                }
            );

            this.RuleFor(
                challenge => challenge.Participants,
                (faker, model) =>
                {
                    _participantFaker = _participantFaker ?? new ParticipantFaker(model);

                    _participantFaker.UseSeed(faker.Random.Int());

                    return _participantFaker.Generate(faker.ChallegeSetupEntries(model));
                }
            );

            this.RuleFor(challenge => challenge.CreatedAt, (faker, model) => faker.ChallengeCreatedAt(model));

            this.RuleFor(
                challenge => challenge.SynchronizedAt,
                (faker, model) => model.Participants.SelectMany(participant => participant.Matches).Max(participant => participant.SynchronizedAt as DateTime?)
            );

            this.RuleFor(
                challenge => challenge.Buckets,
                (faker, challenge) =>
                {
                    var payout = PayoutFactory.Instance.CreateStrategy(
                            new PayoutEntries(challenge.Setup.PayoutEntries),
                            new EntryFee(CurrencyType.FromValue(challenge.Setup.EntryFeeCurrency), challenge.Setup.EntryFeeAmount)
                        )
                        .Payout;

                    return payout.Buckets.Select(
                            sc => new BucketModel
                            {
                                Size = sc.Size,
                                PrizeAmount = sc.Prize.Amount,
                                PrizeCurrency = sc.Prize.Type.Value
                            }
                        )
                        .ToList();
                }
            );

            this.RuleFor(challenge => challenge.Seed, localSeed);

            //this.FinishWith(
            //    (faker, challengeModel) =>
            //    {
            //        challengeModel.Timeline = new TimelineModel
            //        {
            //            StartedAt = challengeModel.Timeline
            //        };
            //    }
            //);
        }

        [NotNull]
        public override ChallengeModel Generate(string ruleSets = null)
        {
            _participantFaker = null;

            return base.Generate(ruleSets);
        }

        public sealed class ParticipantFaker : Faker<ParticipantModel>
        {
            private MatchFaker _matchFaker;

            public ParticipantFaker(ChallengeModel challengeModel)
            {
                this.RuleFor(participant => participant.Id, faker => faker.ParticipantId().ToGuid());

                this.RuleFor(participant => participant.UserId, faker => faker.UserId().ToGuid());

                this.RuleFor(participant => participant.GameAccountId, faker => faker.UserGameReference(ChallengeGame.FromValue(challengeModel.Game)).ToString());

                this.RuleFor(
                    participant => participant.Matches,
                    faker =>
                    {
                        _matchFaker = _matchFaker ?? new MatchFaker(challengeModel);

                        _matchFaker.UseSeed(faker.Random.Int());

                        // TODO: To refactor.
                        var timeline = new ChallengeTimeline(
                            new ChallengeDuration(TimeSpan.FromTicks(challengeModel.Timeline.Duration)),
                            challengeModel.Timeline.StartedAt,
                            challengeModel.Timeline.ClosedAt
                        );

                        return timeline.State != ChallengeState.Inscription
                            ? _matchFaker.Generate(faker.Random.Int(1, challengeModel.Setup.BestOf + 3))
                            : new List<MatchModel>();
                    }
                );

                this.RuleFor(participant => participant.RegisteredAt, faker => faker.ParticipantTimestamp(challengeModel));

                this.RuleFor(participant => participant.SynchronizedAt, (faker, participant) => participant.Matches.Max(match => match.SynchronizedAt as DateTime?));
            }

            [NotNull]
            public override ParticipantModel Generate(string ruleSets = null)
            {
                _matchFaker = null;

                return base.Generate(ruleSets);
            }

            public sealed class MatchFaker : Faker<MatchModel>
            {
                public MatchFaker(ChallengeModel challengeModel)
                {
                    this.RuleFor(match => match.Id, faker => faker.MatchId());

                    this.RuleFor(match => match.GameMatchId, faker => faker.MatchReference(ChallengeGame.FromValue(challengeModel.Game)).ToString());

                    this.RuleFor(
                        match => match.Stats,
                        faker =>
                        {
                            var matchStats = faker.MatchStats(ChallengeGame.FromValue(challengeModel.Game));

                            var scoring = challengeModel.ScoringItems;

                            return SnapshotStats(scoring, matchStats);
                        }
                    );

                    this.RuleFor(match => match.SynchronizedAt, (faker, match) => faker.MatchTimestamp(challengeModel.Timeline));
                }

                // TODO: To refactor.
                public static ICollection<StatModel> SnapshotStats(ICollection<ScoringItemModel> scoring, IMatchStats stats)
                {
                    var statModels = new Collection<StatModel>();

                    for (var index = 0; index < scoring.Count; index++)
                    {
                        var item = scoring.ElementAt(index);

                        var name = item.Name;

                        if (!stats.ContainsKey(new StatName(name)))
                        {
                            continue;
                        }

                        var value = stats[new StatName(name)];

                        var weighting = item.Weighting;

                        var stat = new StatModel
                        {
                            Name = name,
                            Value = value,
                            Weighting = weighting
                        };

                        statModels.Add(stat);
                    }

                    return statModels;
                }
            }
        }
    }
}

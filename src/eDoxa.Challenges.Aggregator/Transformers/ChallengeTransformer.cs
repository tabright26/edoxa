// Filename: ChallengeTransformer.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Challenges.Aggregator.Models;

using ChallengeResponses = eDoxa.Challenges.Responses;
using CashierResponses = eDoxa.Cashier.Responses;
using IdentityResponses = eDoxa.Identity.Responses;

namespace eDoxa.Challenges.Aggregator.Transformers
{
    public static class ChallengeTransformer
    {
        public static ParticipantModel Transform(
            Guid challengeId,
            ChallengeResponses.ParticipantResponse participantFromChallengesService,
            IEnumerable<IdentityResponses.UserDoxatagResponse> doxatagsFromIdentityService
        )
        {
            return new ParticipantModel
            {
                Id = participantFromChallengesService.Id,
                User = new UserModel
                {
                    Id = participantFromChallengesService.UserId,
                    Doxatag = doxatagsFromIdentityService.Where(doxatag => doxatag.UserId == participantFromChallengesService.UserId)
                        .Select(
                            doxatag => new DoxatagModel
                            {
                                Name = doxatag.Name,
                                Code = doxatag.Code
                            })
                        .Single()
                },
                Score = participantFromChallengesService.Score,
                ChallengeId = challengeId,
                Matches = participantFromChallengesService.Matches.Select(
                        match => new MatchModel
                        {
                            Id = match.Id,
                            Score = match.Score,
                            ParticipantId = match.ParticipantId,
                            Stats = match.Stats.Select(
                                    stat => new StatModel
                                    {
                                        Name = stat.Name,
                                        Value = stat.Value,
                                        Weighting = stat.Weighting,
                                        Score = stat.Score
                                    })
                                .ToArray()
                        })
                    .ToArray()
            };
        }

        public static ChallengeModel Transform(
            ChallengeResponses.ChallengeResponse challengeFromChallengesService,
            CashierResponses.ChallengeResponse challengeFromCashierService,
            IEnumerable<IdentityResponses.UserDoxatagResponse> doxatagsFromIdentityService
        )
        {
            return new ChallengeModel
            {
                Id = challengeFromChallengesService.Id,
                Name = challengeFromChallengesService.Name,
                Game = challengeFromChallengesService.Game,
                State = challengeFromChallengesService.State,
                BestOf = challengeFromChallengesService.BestOf,
                Entries = challengeFromChallengesService.Entries,
                EntryFee = new EntryFeeModel
                {
                    Amount = challengeFromCashierService.EntryFee.Amount,
                    Currency = challengeFromCashierService.EntryFee.Currency
                },
                Timeline = new TimelineModel
                {
                    CreatedAt = challengeFromChallengesService.Timeline.CreatedAt.Ticks,
                    StartedAt = challengeFromChallengesService.Timeline.StartedAt?.Ticks,
                    EndedAt = challengeFromChallengesService.Timeline.EndedAt?.Ticks,
                    ClosedAt = challengeFromChallengesService.Timeline.ClosedAt?.Ticks
                },
                Scoring = challengeFromChallengesService.Scoring,
                Payout = new PayoutModel
                {
                    PrizePool = new PrizePoolModel
                    {
                        Currency = challengeFromCashierService.Payout.PrizePool.Currency,
                        Amount = challengeFromCashierService.Payout.PrizePool.Amount
                    },
                    Buckets = challengeFromCashierService.Payout.Buckets.Select(
                            bucket => new BucketModel
                            {
                                Prize = bucket.Prize,
                                Size = bucket.Size
                            })
                        .ToArray()
                },
                Participants = challengeFromChallengesService.Participants
                    .Select(participant => Transform(challengeFromChallengesService.Id, participant, doxatagsFromIdentityService))
                    .ToArray()
            };
        }

        public static IReadOnlyCollection<ChallengeModel> Transform(
            IReadOnlyCollection<ChallengeResponses.ChallengeResponse> challengesFromChallengesService,
            IReadOnlyCollection<CashierResponses.ChallengeResponse> challengesFromCashierService,
            IReadOnlyCollection<IdentityResponses.UserDoxatagResponse> doxatagsFromIdentityService
        )
        {
            var challengeModels = from challengeFromChallengesService in challengesFromChallengesService
                                  let challengeFromCashierService =
                                      challengesFromCashierService.Single(challenge => challenge.Id == challengeFromChallengesService.Id)
                                  select Transform(challengeFromChallengesService, challengeFromCashierService, doxatagsFromIdentityService);

            return challengeModels.ToList();
        }
    }
}

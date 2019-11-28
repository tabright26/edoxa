// Filename: ChallengeTransformer.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Challenges.Web.Aggregator.Models;

using ChallengeResponses = eDoxa.Challenges.Responses;
using CashierResponses = eDoxa.Cashier.Responses;
using IdentityResponses = eDoxa.Identity.Responses;

namespace eDoxa.Challenges.Web.Aggregator.Transformers
{
    public static class ChallengeTransformer
    {
        public static ParticipantModel Transform(
            Guid challengeId,
            ChallengeResponses.ParticipantResponse participantFromChallengesService,
            IEnumerable<IdentityResponses.DoxatagResponse> doxatags
        )
        {
            return new ParticipantModel
            {
                Id = participantFromChallengesService.Id,
                User = new UserModel
                {
                    Id = participantFromChallengesService.UserId,
                    Doxatag = doxatags.Where(doxatag => doxatag.UserId == participantFromChallengesService.UserId)
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
            ChallengeResponses.ChallengeResponse challenge,
            CashierResponses.ChallengeResponse challengeFromCashier,
            IEnumerable<IdentityResponses.DoxatagResponse> doxatags
        )
        {
            return new ChallengeModel
            {
                Id = challenge.Id,
                Name = challenge.Name,
                Game = challenge.Game,
                State = challenge.State,
                BestOf = challenge.BestOf,
                Entries = challenge.Entries,
                EntryFee = new EntryFeeModel
                {
                    Amount = challengeFromCashier.EntryFee.Amount,
                    Currency = challengeFromCashier.EntryFee.Currency
                },
                Timeline = new TimelineModel
                {
                    CreatedAt = new DateTimeOffset(challenge.Timeline.CreatedAt).ToUnixTimeSeconds(),
                    StartedAt = challenge.Timeline.StartedAt.HasValue
                        ? new DateTimeOffset(challenge.Timeline.StartedAt.Value).ToUnixTimeSeconds()
                        : (long?) null,
                    EndedAt =
                        challenge.Timeline.EndedAt.HasValue ? new DateTimeOffset(challenge.Timeline.EndedAt.Value).ToUnixTimeSeconds() : (long?) null,
                    ClosedAt = challenge.Timeline.ClosedAt.HasValue
                        ? new DateTimeOffset(challenge.Timeline.ClosedAt.Value).ToUnixTimeSeconds()
                        : (long?) null
                },
                PayoutEntries = challengeFromCashier.Payout.Buckets.Sum(bucket => bucket.Size),
                SynchronizedAt = challenge.SynchronizedAt.HasValue ? new DateTimeOffset(challenge.SynchronizedAt.Value).ToUnixTimeSeconds() : (long?) null,
                Scoring = challenge.Scoring,
                Payout = new PayoutModel
                {
                    PrizePool = new PrizePoolModel
                    {
                        Currency = challengeFromCashier.Payout.PrizePool.Currency,
                        Amount = challengeFromCashier.Payout.PrizePool.Amount
                    },
                    Buckets = challengeFromCashier.Payout.Buckets.Select(
                            bucket => new BucketModel
                            {
                                Prize = bucket.Prize,
                                Size = bucket.Size
                            })
                        .ToArray()
                },
                Participants = challenge.Participants.Select(participant => Transform(challenge.Id, participant, doxatags)).ToArray()
            };
        }

        public static IReadOnlyCollection<ChallengeModel> Transform(
            IReadOnlyCollection<ChallengeResponses.ChallengeResponse> challengesFromChallengesService,
            IReadOnlyCollection<CashierResponses.ChallengeResponse> challengesFromCashierService,
            IReadOnlyCollection<IdentityResponses.DoxatagResponse> doxatagsFromIdentityService
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

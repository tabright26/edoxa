// Filename: ChallengeTransformer.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Challenges.Web.Aggregator.Models;

using ChallengeDtos = eDoxa.Grpc.Protos.Challenges.Dtos;
using CashierDtos = eDoxa.Grpc.Protos.Cashier.Dtos;
using IdentityDtos = eDoxa.Grpc.Protos.Identity.Dtos;

namespace eDoxa.Challenges.Web.Aggregator.Transformers
{
    public static class ChallengeTransformer
    {
        public static ParticipantModel Transform(string challengeId, ChallengeDtos.ParticipantDto participant, IEnumerable<IdentityDtos.DoxatagDto> doxatags)
        {
            return new ParticipantModel
            {
                Id = participant.Id,
                User = new UserModel
                {
                    Id = participant.UserId,
                    Doxatag = doxatags.Where(doxatag => doxatag.UserId == participant.UserId)
                        .Select(
                            doxatag => new DoxatagModel
                            {
                                Name = doxatag.Name,
                                Code = doxatag.Code
                            })
                        .SingleOrDefault()
                },
                Score = participant.Score,
                ChallengeId = challengeId,
                Matches = participant.Matches.Select(
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
            ChallengeDtos.ChallengeDto challenge,
            CashierDtos.ChallengePayoutDto payout,
            IEnumerable<IdentityDtos.DoxatagDto> doxatags
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
                    Amount = payout.EntryFee.Amount,
                    Currency = payout.EntryFee.Currency
                },
                Timeline = new TimelineModel
                {
                    CreatedAt = challenge.Timeline.CreatedAt.ToDateTimeOffset().ToUnixTimeSeconds(),
                    StartedAt = challenge.Timeline.StartedAt?.ToDateTimeOffset().ToUnixTimeSeconds(),
                    EndedAt = challenge.Timeline.EndedAt?.ToDateTimeOffset().ToUnixTimeSeconds(),
                    ClosedAt = challenge.Timeline.ClosedAt?.ToDateTimeOffset().ToUnixTimeSeconds()
                },
                PayoutEntries = payout.Buckets.Sum(bucket => bucket.Size),
                SynchronizedAt = challenge.SynchronizedAt?.ToDateTimeOffset().ToUnixTimeSeconds(),
                Scoring = challenge.Scoring,
                Payout = new PayoutModel
                {
                    PrizePool = new PrizePoolModel
                    {
                        Currency = payout.PrizePool.Currency,
                        Amount = payout.PrizePool.Amount
                    },
                    Buckets = payout.Buckets.Select(
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
            IReadOnlyCollection<ChallengeDtos.ChallengeDto> challenges,
            IReadOnlyCollection<CashierDtos.ChallengePayoutDto> payouts,
            IReadOnlyCollection<IdentityDtos.DoxatagDto> doxatags
        )
        {
            var challengeModels = from challenge in challenges
                                  let challengePayout = payouts.Single(payout => payout.ChallengeId == challenge.Id)
                                  select Transform(challenge, challengePayout, doxatags);

            return challengeModels.ToList();
        }
    }
}

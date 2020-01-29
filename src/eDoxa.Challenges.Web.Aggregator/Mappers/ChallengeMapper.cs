// Filename: ChallengeMapper.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Challenges.Aggregates;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Identity.Dtos;

using static eDoxa.Grpc.Protos.Challenges.Aggregates.ChallengeAggregate.Types;
using static eDoxa.Grpc.Protos.Challenges.Aggregates.ChallengeAggregate.Types.ParticipantAggregate.Types;
using static eDoxa.Grpc.Protos.Challenges.Aggregates.ChallengeAggregate.Types.ParticipantAggregate.Types.UserAggregate.Types;
using static eDoxa.Grpc.Protos.Challenges.Aggregates.ChallengeAggregate.Types.PayoutAggregate.Types;

namespace eDoxa.Challenges.Web.Aggregator.Mappers
{
    public static class ChallengeMapper
    {
        public static ParticipantAggregate Map(string challengeId, ParticipantDto participant, IEnumerable<DoxatagDto> doxatags)
        {
            return new ParticipantAggregate
            {
                Id = participant.Id,
                User = new UserAggregate
                {
                    Id = participant.UserId,
                    Doxatag = doxatags.Where(doxatag => doxatag.UserId == participant.UserId)
                        .Select(
                            doxatag => new DoxatagAggregate
                            {
                                Name = doxatag.Name,
                                Code = doxatag.Code
                            })
                        .SingleOrDefault()
                },
                Score = participant.Score,
                ChallengeId = challengeId,
                Matches =
                {
                    participant.Matches.OrderBy(match => match.GameStartedAt)
                }
            };
        }

        public static ChallengeAggregate Map(ChallengeDto challenge, ChallengePayoutDto payout, IEnumerable<DoxatagDto> doxatags)
        {
            return new ChallengeAggregate
            {
                Id = challenge.Id,
                Name = challenge.Name,
                Game = challenge.Game,
                State = challenge.State,
                BestOf = challenge.BestOf,
                Entries = challenge.Entries,
                Timeline = challenge.Timeline,
                PayoutEntries = payout.Buckets.Sum(bucket => bucket.Size),
                SynchronizedAt = challenge.SynchronizedAt,
                Scoring =
                {
                    challenge.Scoring
                },
                Payout = new PayoutAggregate
                {
                    ChallengeId = challenge.Id,
                    EntryFee = new EntryFeeAggregate
                    {
                        Amount = payout.EntryFee.Amount,
                        Currency = payout.EntryFee.Currency
                    },
                    PrizePool = new PrizePoolAggregate
                    {
                        Currency = payout.PrizePool.Currency,
                        Amount = payout.PrizePool.Amount
                    },
                    Buckets =
                    {
                        payout.Buckets.Select(
                            bucket => new BucketAggregate
                            {
                                Prize = bucket.Prize,
                                Size = bucket.Size
                            })
                    }
                },
                Participants =
                {
                    challenge.Participants.Select(participant => Map(challenge.Id, participant, doxatags))
                        .OrderByDescending(participant => participant.Score?.ToDecimal())
                }
            };
        }

        public static IReadOnlyCollection<ChallengeAggregate> Map(
            IEnumerable<ChallengeDto> challenges,
            IEnumerable<ChallengePayoutDto> payouts,
            IEnumerable<DoxatagDto> doxatags
        )
        {
            var challengeModels = from challenge in challenges
                                  let challengePayout = payouts.Single(payout => payout.ChallengeId == challenge.Id)
                                  select Map(challenge, challengePayout, doxatags);

            return challengeModels.OrderBy(challenge => challenge.State)
                .ThenByDescending(challenge => challenge.Entries)
                .ThenByDescending(challenge => challenge.Participants.Count)
                .ThenBy(challenge => challenge.Payout.EntryFee)
                .ThenByDescending(challenge => challenge.Payout.PrizePool)
                .ThenBy(challenge => challenge.Name)
                .ToList();
        }
    }
}

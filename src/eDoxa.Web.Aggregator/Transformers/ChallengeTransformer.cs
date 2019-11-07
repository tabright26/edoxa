// Filename: ChallengeTransformer.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Web.Aggregator.Models;

using ChallengeResponseFromChallengesService = eDoxa.Challenges.Responses.ChallengeResponse;
using ChallengeResponseFromCashierService = eDoxa.Cashier.Responses.ChallengeResponse;
using UserDoxatagResponseFromIdentityService = eDoxa.Identity.Responses.UserDoxatagResponse;

namespace eDoxa.Web.Aggregator.Transformers
{
    public static class ChallengeTransformer
    {
        public static ChallengeModel Transform(
            ChallengeResponseFromChallengesService challengeFromChallengesService,
            ChallengeResponseFromCashierService challengeFromCashierService,
            IEnumerable<UserDoxatagResponseFromIdentityService> doxatagsFromIdentityService
        )
        {
            return new ChallengeModel
            {
                Id = challengeFromChallengesService.Id,
                Name = challengeFromChallengesService.Name,
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
                Participants = challengeFromChallengesService.Participants.Select(
                        participant => new ParticipantModel
                        {
                            User = new UserModel
                            {
                                Id = participant.UserId,
                                Doxatag = doxatagsFromIdentityService.Where(doxatag => doxatag.UserId == participant.UserId)
                                    .Select(
                                        doxatag => new DoxatagModel
                                        {
                                            Name = doxatag.Name,
                                            Code = doxatag.Code
                                        })
                                    .Single()
                            },
                            Matches = participant.Matches.Select(
                                    match => new MatchModel
                                    {
                                        Stats = match.Stats.Select(stat => new StatModel()).ToArray()
                                    })
                                .ToArray()
                        })
                    .ToArray()
            };
        }

        public static IReadOnlyCollection<ChallengeModel> Transform(
            IReadOnlyCollection<ChallengeResponseFromChallengesService> challengesFromChallengesService,
            IReadOnlyCollection<ChallengeResponseFromCashierService> challengesFromCashierService,
            IReadOnlyCollection<UserDoxatagResponseFromIdentityService> doxatagsFromIdentityService
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

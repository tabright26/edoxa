// Filename: ChallengeGrpcService.cs
// Date Created: 2019-12-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Responses;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Shared.Dtos;
using eDoxa.Seedwork.Domain.Misc;

using Grpc.Core;

namespace eDoxa.Cashier.Api.Services
{
    public sealed class ChallengeGrpcService : ChallengeService.ChallengeServiceBase
    {
        private readonly IChallengeQuery _challengeQuery;
        private readonly IChallengeService _challengeService;

        public ChallengeGrpcService(IChallengeQuery challengeQuery, IChallengeService challengeService)
        {
            _challengeQuery = challengeQuery;
            _challengeService = challengeService;
        }

        public override async Task<FetchChallengesResponse> FetchChallenges(FetchChallengesRequest request, ServerCallContext context)
        {
            var challenges = await _challengeQuery.FetchChallengesAsync();

            context.Status = new Status(StatusCode.OK, "");

            return new FetchChallengesResponse
            {
                Status = new StatusDto
                {
                    Code = (int) context.Status.StatusCode,
                    Message = context.Status.Detail
                },
                Challenges =
                {
                    challenges.Select(MapChallenge)
                }
            };
        }

        public override async Task<FindChallengeResponse> FindChallenge(FindChallengeRequest request, ServerCallContext context)
        {
            var challenge = await _challengeQuery.FindChallengeAsync(ChallengeId.Parse(request.ChallengeId));

            if (challenge == null)
            {
                context.Status = new Status(StatusCode.NotFound, "");

                return new FindChallengeResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                }; 
            }

            context.Status = new Status(StatusCode.OK, "");

            return new FindChallengeResponse
            {
                Status = new StatusDto
                {
                    Code = (int) context.Status.StatusCode,
                    Message = context.Status.Detail
                },
                Challenge = MapChallenge(challenge)
            };
        }

        public override async Task<CreateChallengeResponse> CreateChallenge(CreateChallengeRequest request, ServerCallContext context)
        {
            var result = await _challengeService.CreateChallengeAsync(
                ChallengeId.Parse(request.Id),
                new PayoutEntries(request.PayoutEntries),
                new EntryFee(Convert.ToDecimal(request.EntryFee.Amount), Domain.AggregateModels.Currency.FromValue((int) request.EntryFee.Currency)));

            if (result.IsValid)
            {
                context.Status = new Status(StatusCode.OK, "");

                return new CreateChallengeResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Challenge = MapChallenge(result.GetEntityFromMetadata<IChallenge>())
                };
            }

            context.Status = new Status(StatusCode.FailedPrecondition, "");

            return new CreateChallengeResponse
            {
                Status = new StatusDto
                {
                    Code = (int) context.Status.StatusCode,
                    Message = context.Status.Detail
                }
            };
        }

        public static ChallengeDto MapChallenge(IChallenge challenge)
        {
            return new ChallengeDto
            {
                Id = challenge.Id.ToString(),
                EntryFee = new EntryFeeDto
                {
                    Amount = Convert.ToDouble(challenge.EntryFee.Amount),
                    Currency = (Currency) challenge.EntryFee.Currency.Value
                },
                Payout = new PayoutDto
                {
                    PrizePool = new PayoutDto.Types.PrizePoolDto
                    {
                        Amount = Convert.ToDouble(challenge.Payout.PrizePool.Amount),
                        Currency = (Currency) challenge.Payout.PrizePool.Currency.Value
                    },
                    Buckets =
                    {
                        challenge.Payout.Buckets.Select(
                                bucket => new PayoutDto.Types.BucketDto
                                {
                                    Prize = Convert.ToDouble(bucket.Prize.Amount),
                                    Size = bucket.Size
                                })
                            .ToList()
                    }
                }
            };
        }
    }
}

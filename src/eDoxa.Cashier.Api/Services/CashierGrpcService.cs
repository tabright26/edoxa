// Filename: CashierGrpcService.cs
// Date Created: 2019-12-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Responses;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Shared.Dtos;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using TransactionStatus = eDoxa.Grpc.Protos.Cashier.Enums.TransactionStatus;

namespace eDoxa.Cashier.Api.Services
{
    public sealed class CashierGrpcService : CashierService.CashierServiceBase
    {
        private readonly IChallengeQuery _challengeQuery;
        private readonly IChallengeService _challengeService;
        private readonly IAccountService _accountService;

        public CashierGrpcService(IChallengeQuery challengeQuery, IChallengeService challengeService, IAccountService accountService)
        {
            _challengeQuery = challengeQuery;
            _challengeService = challengeService;
            _accountService = accountService;
        }

        public override async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var account = await _accountService.FindAccountAsync(userId);

            if (account == null)
            {
                context.Status = new Status(StatusCode.NotFound, "User account not found.");

                return new CreateTransactionResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var result = await _accountService.CreateTransactionAsync(
                account,
                new decimal(request.Amount),
                Currency.FromValue((int) request.Currency),
                TransactionType.FromValue((int) request.Type),
                new TransactionMetadata(request.Metadata));

            if (result.IsValid)
            {
                context.Status = new Status(StatusCode.OK, "");

                return new CreateTransactionResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Transaction = MapTransaction(result.GetEntityFromMetadata<ITransaction>())
                };
            }

            context.Status = new Status(StatusCode.FailedPrecondition, "");

            return new CreateTransactionResponse
            {
                Status = new StatusDto
                {
                    Code = (int) context.Status.StatusCode,
                    Message = context.Status.Detail
                }
            };
        }

        public static TransactionDto MapTransaction(ITransaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id.ToString(),
                Timestamp = Timestamp.FromDateTime(transaction.Timestamp),
                Currency = (Grpc.Protos.Cashier.Enums.Currency) transaction.Currency.Type.Value,
                Amount = Convert.ToDouble(transaction.Currency.Amount),
                Type = (Grpc.Protos.Cashier.Enums.TransactionType) transaction.Type.Value,
                Status = (TransactionStatus) transaction.Status.Value,
                Description = transaction.Description.Text
            };
        }

        public override async Task<FetchChallengePayoutsResponse> FetchChallengePayouts(FetchChallengePayoutsRequest request, ServerCallContext context)
        {
            var challenges = await _challengeQuery.FetchChallengesAsync();

            context.Status = new Status(StatusCode.OK, "");

            return new FetchChallengePayoutsResponse
            {
                Status = new StatusDto
                {
                    Code = (int) context.Status.StatusCode,
                    Message = context.Status.Detail
                },
                Payouts =
                {
                    challenges.Select(MapChallenge)
                }
            };
        }

        public override async Task<FindChallengePayoutResponse> FindChallengePayout(FindChallengePayoutRequest request, ServerCallContext context)
        {
            var challenge = await _challengeQuery.FindChallengeAsync(ChallengeId.Parse(request.ChallengeId));

            if (challenge == null)
            {
                context.Status = new Status(StatusCode.NotFound, "");

                return new FindChallengePayoutResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            context.Status = new Status(StatusCode.OK, "");

            return new FindChallengePayoutResponse
            {
                Status = new StatusDto
                {
                    Code = (int) context.Status.StatusCode,
                    Message = context.Status.Detail
                },
                Payout = MapChallenge(challenge)
            };
        }

        public override async Task<CreateChallengePayoutResponse> CreateChallengePayout(CreateChallengePayoutRequest request, ServerCallContext context)
        {
            var result = await _challengeService.CreateChallengeAsync(
                ChallengeId.Parse(request.ChallengeId),
                new PayoutEntries(request.PayoutEntries),
                new EntryFee(Convert.ToDecimal(request.EntryFee.Amount), Currency.FromValue((int) request.EntryFee.Currency)));

            if (result.IsValid)
            {
                context.Status = new Status(StatusCode.OK, "");

                return new CreateChallengePayoutResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Payout = MapChallenge(result.GetEntityFromMetadata<IChallenge>())
                };
            }

            context.Status = new Status(StatusCode.FailedPrecondition, "");

            return new CreateChallengePayoutResponse
            {
                Status = new StatusDto
                {
                    Code = (int) context.Status.StatusCode,
                    Message = context.Status.Detail
                }
            };
        }

        public static ChallengePayoutDto MapChallenge(IChallenge challenge)
        {
            return new ChallengePayoutDto
            {
                ChallengeId = challenge.Id.ToString(),
                EntryFee = new EntryFeeDto
                {
                    Amount = Convert.ToDouble(challenge.EntryFee.Amount),
                    Currency = (Grpc.Protos.Cashier.Enums.Currency) challenge.EntryFee.Currency.Value
                },
                PrizePool = new ChallengePayoutDto.Types.PrizePoolDto
                {
                    Amount = Convert.ToDouble(challenge.Payout.PrizePool.Amount),
                    Currency = (Grpc.Protos.Cashier.Enums.Currency) challenge.Payout.PrizePool.Currency.Value
                },
                Buckets =
                {
                    challenge.Payout.Buckets.Select(
                            bucket => new ChallengePayoutDto.Types.BucketDto
                            {
                                Prize = Convert.ToDouble(bucket.Prize.Amount),
                                Size = bucket.Size
                            })
                        .ToList()
                }
            };
        }
    }
}

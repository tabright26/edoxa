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
using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Responses;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using static eDoxa.Grpc.Protos.Cashier.Dtos.ChallengePayoutDto.Types;

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
                throw context.RpcException(new Status(StatusCode.NotFound, "User account not found."));
            }

            var result = await _accountService.CreateTransactionAsync(
                account,
                new decimal(request.Amount),
                Currency.FromValue((int) request.Currency),
                TransactionType.FromValue((int) request.Type),
                new TransactionMetadata(request.Metadata));

            if (result.IsValid)
            {
                return context.Ok(
                    new CreateTransactionResponse
                    {
                        Transaction = MapTransaction(result.GetEntityFromMetadata<ITransaction>())
                    });
            }

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, ""));
        }

        public static TransactionDto MapTransaction(ITransaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id.ToString(),
                Timestamp = Timestamp.FromDateTime(transaction.Timestamp),
                Currency = (CurrencyDto) transaction.Currency.Type.Value,
                Amount = Convert.ToDouble(transaction.Currency.Amount),
                Type = (TransactionTypeDto) transaction.Type.Value,
                Status = (TransactionStatusDto) transaction.Status.Value,
                Description = transaction.Description.Text
            };
        }

        public override async Task<FetchChallengePayoutsResponse> FetchChallengePayouts(FetchChallengePayoutsRequest request, ServerCallContext context)
        {
            var challenges = await _challengeQuery.FetchChallengesAsync();

            return context.Ok(
                new FetchChallengePayoutsResponse
                {
                    Payouts =
                    {
                        challenges.Select(MapChallenge)
                    }
                });
        }

        public override async Task<FindChallengePayoutResponse> FindChallengePayout(FindChallengePayoutRequest request, ServerCallContext context)
        {
            var challenge = await _challengeQuery.FindChallengeAsync(ChallengeId.Parse(request.ChallengeId));

            if (challenge == null)
            {
                throw context.RpcException(new Status(StatusCode.NotFound, ""));
            }

            return context.Ok(
                new FindChallengePayoutResponse
                {
                    Payout = MapChallenge(challenge)
                });
        }

        public override async Task<CreateChallengePayoutResponse> CreateChallengePayout(CreateChallengePayoutRequest request, ServerCallContext context)
        {
            var result = await _challengeService.CreateChallengeAsync(
                ChallengeId.Parse(request.ChallengeId),
                new PayoutEntries(request.PayoutEntries),
                new EntryFee(Convert.ToDecimal(request.EntryFee.Amount), Currency.FromValue((int) request.EntryFee.Currency)));

            if (result.IsValid)
            {
                return context.Ok(
                    new CreateChallengePayoutResponse
                    {
                        Payout = MapChallenge(result.GetEntityFromMetadata<IChallenge>())
                    });
            }

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, ""));
        }

        public static ChallengePayoutDto MapChallenge(IChallenge challenge)
        {
            return new ChallengePayoutDto
            {
                ChallengeId = challenge.Id,
                EntryFee = new EntryFeeDto
                {
                    Amount = Convert.ToDouble(challenge.EntryFee.Amount),
                    Currency = (CurrencyDto) challenge.EntryFee.Currency.Value
                },
                PrizePool = new PrizePoolDto
                {
                    Amount = Convert.ToDouble(challenge.Payout.PrizePool.Amount),
                    Currency = (CurrencyDto) challenge.Payout.PrizePool.Currency.Value
                },
                Buckets =
                {
                    challenge.Payout.Buckets.Select(
                            bucket => new BucketDto
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

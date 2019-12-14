// Filename: CashierGrpcService.cs
// Date Created: 2019-12-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Responses;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.Grpc.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using Grpc.Core;

namespace eDoxa.Cashier.Api.Services
{
    public sealed class CashierGrpcService : CashierService.CashierServiceBase
    {
        private readonly IChallengeQuery _challengeQuery;
        private readonly IChallengeService _challengeService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public CashierGrpcService(IChallengeQuery challengeQuery, IChallengeService challengeService, IAccountService accountService, IMapper mapper)
        {
            _challengeQuery = challengeQuery;
            _challengeService = challengeService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public override async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var account = await _accountService.FindAccountAsync(userId);

            if (account == null)
            {
                throw context.NotFoundRpcException("User account not found.");
            }

            var result = await _accountService.CreateTransactionAsync(
                account,
                request.Amount,
                request.Currency.ToEnumeration<Currency>(),
                request.Type.ToEnumeration<TransactionType>(),
                new TransactionMetadata(request.Metadata));

            if (result.IsValid)
            {
                var response = new CreateTransactionResponse
                {
                    Transaction = _mapper.Map<TransactionDto>(result.GetEntityFromMetadata<ITransaction>())
                };

                return context.Ok(response);
            }

            throw context.FailedPreconditionRpcException(result, string.Empty);
        }

        public override async Task<FetchChallengePayoutsResponse> FetchChallengePayouts(FetchChallengePayoutsRequest request, ServerCallContext context)
        {
            var challenges = await _challengeQuery.FetchChallengesAsync();

            var response = new FetchChallengePayoutsResponse
            {
                Payouts =
                {
                    _mapper.Map<IEnumerable<ChallengePayoutDto>>(challenges)
                }
            };

            return context.Ok(response);
        }

        public override async Task<FindChallengePayoutResponse> FindChallengePayout(FindChallengePayoutRequest request, ServerCallContext context)
        {
            var challenge = await _challengeQuery.FindChallengeAsync(request.ChallengeId.ParseEntityId<ChallengeId>());

            if (challenge == null)
            {
                throw context.NotFoundRpcException("Challenge not found.");
            }

            var response = new FindChallengePayoutResponse
            {
                Payout = _mapper.Map<ChallengePayoutDto>(challenge)
            };

            return context.Ok(response);
        }

        public override async Task<CreateChallengePayoutResponse> CreateChallengePayout(CreateChallengePayoutRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _challengeService.CreateChallengeAsync(
                    ChallengeId.Parse(request.ChallengeId),
                    new PayoutEntries(request.PayoutEntries),
                    new EntryFee(request.EntryFee.Amount, request.EntryFee.Currency.ToEnumeration<Currency>()));

                if (result.IsValid)
                {
                    var response = new CreateChallengePayoutResponse
                    {
                        Payout = _mapper.Map<ChallengePayoutDto>(result.GetEntityFromMetadata<IChallenge>())
                    };

                    return context.Ok(response);
                }

                throw context.FailedPreconditionRpcException(result, string.Empty);
            }
            catch (Exception exception)
            {
                // TODO: Integration required. SAGA PATTERN.

                throw exception.Capture();
            }
        }

        //private static TransactionDto MapTransaction(ITransaction transaction)
        //{
        //    return new TransactionDto
        //    {
        //        Id = transaction.Id.ToString(),
        //        Timestamp = transaction.Timestamp.ToTimestamp(),
        //        Currency = transaction.Currency.Type.ToEnum<CurrencyDto>(),
        //        Amount = transaction.Currency.Amount,
        //        Type = transaction.Type.ToEnum<TransactionTypeDto>(),
        //        Status = transaction.Status.ToEnum<TransactionStatusDto>(),
        //        Description = transaction.Description.Text
        //    };
        //}

        //private static ChallengePayoutDto MapChallenge(IChallenge challenge)
        //{
        //    return new ChallengePayoutDto
        //    {
        //        ChallengeId = challenge.Id,
        //        EntryFee = new EntryFeeDto
        //        {
        //            Amount = challenge.EntryFee.Amount,
        //            Currency = challenge.EntryFee.Currency.ToEnum<CurrencyDto>()
        //        },
        //        PrizePool = new PrizePoolDto
        //        {
        //            Amount = challenge.Payout.PrizePool.Amount,
        //            Currency = challenge.Payout.PrizePool.Currency.ToEnum<CurrencyDto>()
        //        },
        //        Buckets =
        //        {
        //            challenge.Payout.Buckets.Select(
        //                    bucket => new BucketDto
        //                    {
        //                        Prize = bucket.Prize.Amount,
        //                        Size = bucket.Size
        //                    })
        //                .ToList()
        //        }
        //    };
        //}
    }
}

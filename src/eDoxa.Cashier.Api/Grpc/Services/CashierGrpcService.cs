// Filename: CashierGrpcService.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
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
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Grpc.Core;

namespace eDoxa.Cashier.Api.Grpc.Services
{
    public sealed class CashierGrpcService : CashierService.CashierServiceBase
    {
        private readonly IChallengeQuery _challengeQuery;
        private readonly IChallengeService _challengeService;
        private readonly IAccountService _accountService;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IMapper _mapper;

        public CashierGrpcService(
            IChallengeQuery challengeQuery,
            IChallengeService challengeService,
            IAccountService accountService,
            IServiceBusPublisher serviceBusPublisher,
            IMapper mapper
        )
        {
            _challengeQuery = challengeQuery;
            _challengeService = challengeService;
            _accountService = accountService;
            _serviceBusPublisher = serviceBusPublisher;
            _mapper = mapper;
        }

        public override async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var account = await _accountService.FindAccountOrNullAsync(userId);

            if (account == null)
            {
                throw context.NotFoundRpcException("User account not found.");
            }

            var result = await CreateTransactionByOneofCaseAsync();

            if (result.IsValid)
            {
                var response = new CreateTransactionResponse
                {
                    Transaction = _mapper.Map<TransactionDto>(result.Response)
                };

                return context.Ok(response);
            }

            throw context.FailedPreconditionRpcException(result);

            async Task<DomainValidationResult<ITransaction>> CreateTransactionByOneofCaseAsync()
            {
                switch (request.TransactionCase)
                {
                    case CreateTransactionRequest.TransactionOneofCase.Bundle:
                    {
                        return await _accountService.CreateTransactionAsync(account!, request.Bundle, new TransactionMetadata(request.Metadata));
                    }

                    case CreateTransactionRequest.TransactionOneofCase.Custom:
                    {
                        return await _accountService.CreateTransactionAsync(
                            account!,
                            request.Custom.Currency.Amount,
                            request.Custom.Currency.Type.ToEnumeration<CurrencyType>(),
                            request.Custom.Type.ToEnumeration<TransactionType>(),
                            new TransactionMetadata(request.Metadata));
                    }
                    default:
                    {
                        throw context.RpcException(
                            new Status(
                                StatusCode.InvalidArgument,
                                $"The case ({request.TransactionCase}) is not supported for {nameof(this.CreateTransaction)}."));
                    }
                }
            }
        }

        public override async Task<CancelTransactionResponse> CancelTransaction(CancelTransactionRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var account = await _accountService.FindAccountOrNullAsync(userId);

            if (account == null)
            {
                throw context.NotFoundRpcException("User account not found.");
            }

            var result = await _accountService.MarkAccountTransactionAsCanceledAsync(account!, request.TransactionId.ParseEntityId<TransactionId>());

            if (result.IsValid)
            {
                var response = new CancelTransactionResponse();

                return context.Ok(response);
            }

            throw context.FailedPreconditionRpcException(result);
        }

        public override async Task<DeleteTransactionResponse> DeleteTransaction(DeleteTransactionRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var account = await _accountService.FindAccountOrNullAsync(userId);

            if (account == null)
            {
                throw context.NotFoundRpcException("User account not found.");
            }

            var result = await _accountService.DeleteTransactionAsync(account!, request.TransactionId.ParseEntityId<TransactionId>());

            if (result.IsValid)
            {
                var response = new DeleteTransactionResponse();

                return context.Ok(response);
            }

            throw context.FailedPreconditionRpcException(result);
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
            var challengeId = request.ChallengeId.ParseEntityId<ChallengeId>();

            var result = await _challengeService.CreateChallengeAsync(
                challengeId,
                new ChallengePayoutEntries(request.PayoutEntries),
                new EntryFee(request.EntryFee.Amount, request.EntryFee.Type.ToEnumeration<CurrencyType>()));

            if (result.IsValid)
            {
                var response = new CreateChallengePayoutResponse
                {
                    Payout = _mapper.Map<ChallengePayoutDto>(result.Response)
                };

                return context.Ok(response);
            }

            await _serviceBusPublisher.PublishCreateChallengePayoutFailedIntegrationEventAsync(challengeId);

            throw context.FailedPreconditionRpcException(result);
        }
    }
}

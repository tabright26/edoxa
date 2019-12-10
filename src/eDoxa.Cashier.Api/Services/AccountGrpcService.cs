// Filename: AccountService.cs
// Date Created: 2019-12-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Grpc.Protos;
using eDoxa.Seedwork.Application.Extensions;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

namespace eDoxa.Cashier.Api.Services
{
    public sealed class AccountGrpcService : AccountService.AccountServiceBase
    {
        private readonly IAccountService _accountService;

        public AccountGrpcService(IAccountService accountService)
        {
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
                Domain.AggregateModels.Currency.FromValue((int) request.Currency),
                Seedwork.Domain.Misc.TransactionType.FromValue((int) request.Type),
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
                Currency = (Currency) transaction.Currency.Type.Value,
                Amount = Convert.ToDouble(transaction.Currency.Amount),
                Type = (TransactionType) transaction.Type.Value,
                Status = (TransactionStatus) transaction.Status.Value,
                Description = transaction.Description.Text
            };
        }
    }
}

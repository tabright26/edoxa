// Filename: TransactionControllers.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Grpc.Protos.Payment.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Web.Aggregator.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/transactions")]
    [ApiExplorerSettings(GroupName = "Transactions")]
    public sealed class TransactionsControllers : ControllerBase
    {
        private readonly CashierService.CashierServiceClient _cashierServiceClient;
        private readonly PaymentService.PaymentServiceClient _paymentServiceClient;

        public TransactionsControllers(CashierService.CashierServiceClient cashierServiceClient, PaymentService.PaymentServiceClient paymentServiceClient)
        {
            _cashierServiceClient = cashierServiceClient;
            _paymentServiceClient = paymentServiceClient;
        }

        [HttpPost]
        [SwaggerOperation("Create a transaction.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TransactionDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAsync([FromBody] CreateTransactionAggregatorRequest request)
        {
            var createTransactionRequest = new CreateTransactionRequest
            {
                Bundle = request.Bundle
            };

            var createTransactionResponse = await _cashierServiceClient.CreateTransactionAsync(createTransactionRequest);

            var transaction = createTransactionResponse.Transaction;

            if (transaction.Type == EnumTransactionType.Deposit)
            {
                var depositRequest = new DepositRequest
                {
                    Transaction = transaction
                };

                await _paymentServiceClient.DepositAsync(depositRequest);
            }

            if (transaction.Type == EnumTransactionType.Withdrawal)
            {
                var withdrawalRequest = new WithdrawalRequest
                {
                    Transaction = transaction
                };

                await _paymentServiceClient.WithdrawalAsync(withdrawalRequest);
            }

            return this.Ok(transaction);
        }
    }
}

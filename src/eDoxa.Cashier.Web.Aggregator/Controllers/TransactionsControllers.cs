// Filename: TransactionsControllers.cs
// Date Created: 2020-01-11
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Web.Aggregator.Requests;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Grpc.Protos.Cashier.Services;
using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Grpc.Protos.Payment.Services;
using eDoxa.Seedwork.Domain.Extensions;

using Grpc.Core;

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

        [HttpPost("deposit")]
        [SwaggerOperation("Create a deposit transaction.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DepositAsync([FromBody] DepositTransactionRequest request)
        {
            var createTransactionRequest = new CreateTransactionRequest
            {
                Bundle = request.BundleId
            };

            var createTransactionResponse = await _cashierServiceClient.CreateTransactionAsync(createTransactionRequest);

            try
            {
                var createStripePaymentIntentRequest = new CreateStripePaymentIntentRequest
                {
                    PaymentMethodId = request.PaymentMethodId,
                    Transaction = createTransactionResponse.Transaction
                };

                var createStripePaymentIntentResponse = await _paymentServiceClient.CreateStripePaymentIntentAsync(createStripePaymentIntentRequest);

                return this.Ok(createStripePaymentIntentResponse.ClientSecret);
            }
            catch (RpcException exception)
            {
                var cancelTransactionRequest = new CancelTransactionRequest
                {
                    TransactionId = createTransactionResponse.Transaction.Id
                };

                await _cashierServiceClient.CancelTransactionAsync(cancelTransactionRequest);

                throw exception.Capture();
            }
        }

        [HttpPost("withdraw")]
        [SwaggerOperation("Create a withdraw transaction.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> WithdrawAsync([FromBody] WithdrawTransactionRequest request)
        {
            var createTransactionRequest = new CreateTransactionRequest
            {
                Bundle = request.BundleId
            };

            var createTransactionResponse = await _cashierServiceClient.CreateTransactionAsync(createTransactionRequest);

            try
            {
                var createPaypalPayoutRequest = new CreatePaypalPayoutRequest
                {
                    Email = request.Email,
                    Transaction = createTransactionResponse.Transaction
                };

                await _paymentServiceClient.CreatePaypalPayoutAsync(createPaypalPayoutRequest);

                return this.Ok();
            }
            catch (RpcException exception)
            {
                var cancelTransactionRequest = new CancelTransactionRequest
                {
                    TransactionId = createTransactionResponse.Transaction.Id
                };

                await _cashierServiceClient.CancelTransactionAsync(cancelTransactionRequest);

                throw exception.Capture();
            }
        }
    }
}

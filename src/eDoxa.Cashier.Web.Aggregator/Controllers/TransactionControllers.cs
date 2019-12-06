﻿// Filename: TransactionControllers.cs
// Date Created: 2019-12-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos;

using Microsoft.AspNetCore.Mvc;

using static eDoxa.Grpc.Protos.PaymentService;

namespace eDoxa.Cashier.Web.Aggregator.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/balance")]
    [ApiExplorerSettings(GroupName = "Balance")]
    public sealed class TransactionControllers : ControllerBase
    {
        private readonly PaymentServiceClient _paymentServiceClient;

        public TransactionControllers(PaymentServiceClient paymentServiceClient)
        {
            _paymentServiceClient = paymentServiceClient;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync()
        {
            var request = new DepositRequest();

            var response = await _paymentServiceClient.DepositAsync(request);

            return this.Ok(response);
        }
    }
}

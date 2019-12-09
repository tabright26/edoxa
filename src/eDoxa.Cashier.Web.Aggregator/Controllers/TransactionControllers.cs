// Filename: TransactionControllers.cs
// Date Created: 2019-12-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Grpc.Protos;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Web.Aggregator.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/balance")]
    [ApiExplorerSettings(GroupName = "Balance")]
    public sealed class TransactionControllers : ControllerBase
    {
        private readonly PaymentService.PaymentServiceClient _paymentServiceClient;

        public TransactionControllers(PaymentService.PaymentServiceClient paymentServiceClient)
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

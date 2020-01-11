// Filename: TransactionBundlesController.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Controllers
{
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Route("api/transaction-bundles")]
    [ApiExplorerSettings(GroupName = "Transactions")]
    public sealed class TransactionBundlesController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public TransactionBundlesController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [SwaggerOperation("Fetch transaction bundles.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(TransactionBundleDto[]))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync([FromQuery] EnumTransactionType transactionType, [FromQuery] EnumCurrency currency)
        {
            var transactionBundles = await _accountService.FetchTransactionBundlesAsync(transactionType, currency, true);

            if (!transactionBundles.Any())
            {
                return this.NotFound();
            }

            return this.Ok(transactionBundles);
        }
    }
}

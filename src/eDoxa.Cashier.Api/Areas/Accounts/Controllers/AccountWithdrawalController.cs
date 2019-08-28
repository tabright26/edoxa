// Filename: AccountWithdrawalController.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Accounts.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/account/withdrawal")]
    [ApiExplorerSettings(GroupName = "Account")]
    public sealed class AccountWithdrawalController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountWithdrawalController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        ///     Withdrawal money from the account.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] AccountWithdrawalPostRequest postRequest)
        {
            var userId = HttpContext.GetUserId();

            var connectAccountId = HttpContext.GetConnectAccountId()!;

            await _accountService.WithdrawalAsync(connectAccountId, userId, new Money(postRequest.Amount));

            return this.Ok("Processing the withdrawal transaction...");
        }
    }
}

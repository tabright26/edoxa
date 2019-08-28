// Filename: AccountDepositController.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
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
    [Route("api/account/deposit")]
    [ApiExplorerSettings(GroupName = "Account")]
    public sealed class AccountDepositController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountDepositController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        ///     Deposit currency on the account.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] AccountDepositPostRequest postRequest)
        {
            var userId = HttpContext.GetUserId();

            var customerId = HttpContext.GetCustomerId()!;

            await _accountService.DepositAsync(customerId, userId, MapCurrency(postRequest.Currency, postRequest.Amount));

            return this.Ok("Processing the deposit transaction...");
        }

        private static ICurrency MapCurrency(string currency, decimal amount)
        {
            if (Currency.FromName(currency) == Currency.Money)
            {
                return new Money(amount);
            }

            if (Currency.FromName(currency) == Currency.Token)
            {
                return new Token(amount);
            }

            throw new InvalidOperationException();
        }
    }
}

﻿// Filename: AccountController.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.Infrastructure.Queries.Extensions;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common.Enumerations;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/account")]
    [ApiExplorerSettings(GroupName = "Account")]
    public sealed class AccountController : ControllerBase
    {
        private readonly IAccountQuery _accountQuery;
        private readonly IMediator _mediator;

        public AccountController(IAccountQuery accountQuery, IMediator mediator)
        {
            _accountQuery = accountQuery;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get account balance by currency.
        /// </summary>
        [HttpGet("balance/{currency}", Name = nameof(GetBalanceAsync))]
        public async Task<IActionResult> GetBalanceAsync(CurrencyType currency)
        {
            var balanceViewModel = await _accountQuery.FindUserBalanceViewModelAsync(currency);

            if (balanceViewModel == null)
            {
                return this.NotFound($"Account balance for currency {currency} not found.");
            }

            return this.Ok(balanceViewModel);
        }

        /// <summary>
        ///     Deposit currency on the account.
        /// </summary>
        [HttpPost("deposit", Name = nameof(DepositAsync))]
        public async Task<IActionResult> DepositAsync([FromBody] DepositCommand command)
        {
            var transaction = await _mediator.SendCommandAsync(command);

            return this.Ok(transaction);
        }

        /// <summary>
        ///     Withdraw money from the account.
        /// </summary>
        [HttpPost("withdraw", Name = nameof(WithdrawAsync))]
        public async Task<IActionResult> WithdrawAsync([FromBody] WithdrawCommand command)
        {
            var transaction = await _mediator.SendCommandAsync(command);

            return this.Ok(transaction);
        }
    }
}

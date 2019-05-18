// Filename: AccountMoneyController.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Commands.Extensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/accounts/money")]
    [ApiExplorerSettings(GroupName = "Accounts")]
    public sealed class AccountMoneyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountMoneyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Deposit money on the account.
        /// </summary>
        [HttpPost("deposit", Name = nameof(DepositMoneyAsync))]
        public async Task<IActionResult> DepositMoneyAsync([FromBody] DepositMoneyCommand command)
        {
            var either = await _mediator.SendCommandAsync(command);

            return either.Match<IActionResult>(
                error => this.BadRequest(error.Message),
                this.Ok
            );
        }

        /// <summary>
        ///     Withdraw money from the account.
        /// </summary>
        [HttpPost("withdraw", Name = nameof(WithdrawMoneyAsync))]
        public async Task<IActionResult> WithdrawMoneyAsync([FromBody] WithdrawMoneyCommand command)
        {
            var either = await _mediator.SendCommandAsync(command);

            return either.Match<IActionResult>(
                error => this.BadRequest(error.Message),
                this.Ok
            );
        }
    }
}
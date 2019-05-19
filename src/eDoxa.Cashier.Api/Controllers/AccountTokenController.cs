// Filename: AccountTokenController.cs
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
    [Route("api/accounts/token")]
    [ApiExplorerSettings(GroupName = "Accounts")]
    public class AccountTokenController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountTokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Deposit token on the account.
        /// </summary>
        [HttpPost("deposit", Name = nameof(DepositTokenAsync))]
        public async Task<IActionResult> DepositTokenAsync([FromBody] DepositTokenCommand command)
        {
            var either = await _mediator.SendCommandAsync(command);

            return either.Match<IActionResult>(error => this.BadRequest(error.ToString()), this.Ok);
        }
    }
}

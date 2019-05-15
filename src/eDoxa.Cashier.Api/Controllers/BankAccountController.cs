// Filename: BankAccountController.cs
// Date Created: 2019-05-10
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
    [Route("api/bank-account")]
    public sealed class BankAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = nameof(CreateBankAccountAsync))]
        public async Task<IActionResult> CreateBankAccountAsync([FromBody] CreateBankAccountCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        [HttpDelete(Name = nameof(DeleteBankAccountAsync))]
        public async Task<IActionResult> DeleteBankAccountAsync()
        {
            return await _mediator.SendCommandAsync(new DeleteBankAccountCommand());
        }
    }
}
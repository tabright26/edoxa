// Filename: StripeBankAccountController.cs
// Date Created: 2019-05-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.Services.Stripe.Filters.Attributes;
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
    [Route("api/stripe/bank-account")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeBankAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StripeBankAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Create the Stripe bank account.
        /// </summary>
        [CreateBankAccountResourceFilter]
        [HttpPost(Name = nameof(CreateBankAccountAsync))]
        public async Task<IActionResult> CreateBankAccountAsync([FromBody] CreateBankAccountCommand command)
        {
            var either = await _mediator.SendCommandAsync(command);

            return either.Match<IActionResult>(
                error => this.BadRequest(error.Message),
                success => this.Ok(success.Message)
            );
        }

        /// <summary>
        ///     Delete the Stripe bank account.
        /// </summary>
        [DeleteBankAccountResourceFilter]
        [HttpDelete(Name = nameof(DeleteBankAccountAsync))]
        public async Task<IActionResult> DeleteBankAccountAsync()
        {
            var either = await _mediator.SendCommandAsync(new DeleteBankAccountCommand());

            return either.Match<IActionResult>(
                error => this.BadRequest(error.Message),
                success => this.Ok(success.Message)
            );
        }
    }
}
// Filename: StripeBankAccountController.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Seedwork.Application.Commands.Extensions;
using eDoxa.Stripe.Filters.Attributes;

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
        [StripeResourceFilter]
        [HttpPost(Name = nameof(CreateBankAccountAsync))]
        public async Task<IActionResult> CreateBankAccountAsync([FromBody] CreateBankAccountCommand command)
        {
            await _mediator.SendCommandAsync(command);

            return this.Ok("The bank account has been added.");
        }

        /// <summary>
        ///     Delete the Stripe bank account.
        /// </summary>
        [StripeResourceFilter]
        [HttpDelete(Name = nameof(DeleteBankAccountAsync))]
        public async Task<IActionResult> DeleteBankAccountAsync()
        {
            await _mediator.SendCommandAsync(new DeleteBankAccountCommand());

            return this.Ok("The bank account has been removed.");
        }
    }
}

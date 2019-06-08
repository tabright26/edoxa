// Filename: StripeAccountController.cs
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
using eDoxa.Commands.Extensions;
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
    [Route("api/stripe/account")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StripeAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Verify the Stripe account
        /// </summary>
        [StripeResourceFilter]
        [HttpPatch("verify", Name = nameof(VerifyAccountAsync))]
        public async Task<IActionResult> VerifyAccountAsync([FromBody] VerifyAccountCommand command)
        {
            await _mediator.SendCommandAsync(command);

            return this.Ok("Stripe connect account verify.");
        }
    }
}

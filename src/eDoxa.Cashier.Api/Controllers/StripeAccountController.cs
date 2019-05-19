// Filename: StripeAccountController.cs
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
        [TestUserResourceFilter]
        [HttpPatch("verify", Name = nameof(VerifyAccountAsync))]
        public async Task<IActionResult> VerifyAccountAsync([FromBody] VerifyAccountCommand command)
        {
            var either = await _mediator.SendCommandAsync(command);

            return either.Match<IActionResult>(error => this.BadRequest(error.ToString()), success => this.Ok(success.ToString()));
        }
    }
}

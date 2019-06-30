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
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Filters.Attributes;
using eDoxa.Stripe.Models;

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
        private readonly IUserRepository _userRepository;
        private readonly IStripeService _stripeService;

        public StripeAccountController(IMediator mediator, IUserRepository userRepository, IStripeService stripeService)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _stripeService = stripeService;
        }

        /// <summary>
        ///     Check if the user's Stripe account is verified.
        /// </summary>
        [HttpGet("verify")]
        public async Task<IActionResult> GetAsync()
        {
            var userId = HttpContext.GetUserId();

            // TODO: To refactor.
            var user = await _userRepository.GetUserAsNoTrackingAsync(userId);

            if (user == null)
            {
                return this.NotFound("User not found.");
            }

            var isVerified = await _stripeService.AccountIsVerified(new StripeConnectAccountId(user.ConnectAccountId));

            return this.Ok(isVerified);
        }

        /// <summary>
        ///     Verify the Stripe account
        /// </summary>
        [StripeResourceFilter]
        [HttpPatch("verify")]
        public async Task<IActionResult> PatchAsync([FromBody] VerifyAccountCommand command)
        {
            await _mediator.SendCommandAsync(command);

            return this.Ok("Stripe connect account verify.");
        }
    }
}

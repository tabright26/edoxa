// Filename: StripeCardsController.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Stripe.Filters.Attributes;
using eDoxa.Cashier.DTO.Queries;
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
    [Route("api/stripe/cards")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeCardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStripeCardQueries _stripeCardQueries;

        public StripeCardsController(IStripeCardQueries stripeCardQueries, IMediator mediator)
        {
            _stripeCardQueries = stripeCardQueries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get the Stripe cards.
        /// </summary>
        [HttpGet(Name = nameof(GetCardsAsync))]
        public async Task<IActionResult> GetCardsAsync()
        {
            var cards = await _stripeCardQueries.GetCardsAsync();

            return cards.Select(this.Ok).Cast<IActionResult>().DefaultIfEmpty(this.NoContent()).Single();
        }

        /// <summary>
        ///     Create a Stripe card.
        /// </summary>
        [TestUserResourceFilter]
        [HttpPost(Name = nameof(CreateCardAsync))]
        public async Task<IActionResult> CreateCardAsync([FromBody] CreateCardCommand command)
        {
            var either = await _mediator.SendCommandAsync(command);

            return either.Match<IActionResult>(error => this.BadRequest(error.ToString()), success => this.Ok(success.ToString()));
        }

        /// <summary>
        ///     Delete a Stripe card.
        /// </summary>
        [TestUserResourceFilter]
        [HttpDelete("{cardId}", Name = nameof(DeleteCardAsync))]
        public async Task<IActionResult> DeleteCardAsync(StripeCardId cardId)
        {
            var either = await _mediator.SendCommandAsync(new DeleteCardCommand(cardId));

            return either.Match<IActionResult>(error => this.BadRequest(error.ToString()), success => this.Ok(success.ToString()));
        }

        /// <summary>
        ///     Update the Stripe card default.
        /// </summary>
        [TestUserResourceFilter]
        [HttpPatch("{cardId}/default", Name = nameof(UpdateCardDefaultAsync))]
        public async Task<IActionResult> UpdateCardDefaultAsync(StripeCardId cardId)
        {
            var either = await _mediator.SendCommandAsync(new UpdateCardDefaultCommand(cardId));

            return either.Match<IActionResult>(error => this.BadRequest(error.ToString()), success => this.Ok(success.ToString()));
        }
    }
}

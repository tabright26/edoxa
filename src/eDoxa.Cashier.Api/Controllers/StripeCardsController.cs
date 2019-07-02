// Filename: StripeCardsController.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Commands.Extensions;
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
    [Route("api/stripe/cards")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeCardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICardQuery _cardQuery;

        public StripeCardsController(ICardQuery cardQuery, IMediator mediator)
        {
            _cardQuery = cardQuery;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get the Stripe cards.
        /// </summary>
        [HttpGet(Name = nameof(GetCardsAsync))]
        public async Task<IActionResult> GetCardsAsync()
        {
            var cards = await _cardQuery.GetCardsAsync();

            if (!cards.Any())
            {
                return this.NoContent();
            }

            return this.Ok(cards);
        }

        /// <summary>
        ///     Create a Stripe card.
        /// </summary>
        [StripeResourceFilter]
        [HttpPost(Name = nameof(CreateCardAsync))]
        public async Task<IActionResult> CreateCardAsync([FromBody] CreateCardCommand command)
        {
            await _mediator.SendCommandAsync(command);

            return this.Ok("Credit card added.");
        }

        /// <summary>
        ///     Delete a Stripe card.
        /// </summary>
        [StripeResourceFilter]
        [HttpDelete("{cardId}", Name = nameof(DeleteCardAsync))]
        public async Task<IActionResult> DeleteCardAsync(StripeCardId cardId)
        {
            await _mediator.SendCommandAsync(new DeleteCardCommand(cardId));

            return this.Ok("The card has been removed.");
        }

        /// <summary>
        ///     Update the Stripe card default.
        /// </summary>
        [StripeResourceFilter]
        [HttpPatch("{cardId}/default", Name = nameof(UpdateCardDefaultAsync))]
        public async Task<IActionResult> UpdateCardDefaultAsync(StripeCardId cardId)
        {
            await _mediator.SendCommandAsync(new UpdateCardDefaultCommand(cardId));

            return this.Ok("The card has been updated as default.");
        }
    }
}

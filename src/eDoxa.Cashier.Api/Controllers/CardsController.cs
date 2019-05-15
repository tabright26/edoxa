// Filename: CardsController.cs
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
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/cards")]
    public sealed class CardsController : ControllerBase
    {
        private readonly ICardQueries _cardQueries;
        private readonly IMediator _mediator;
        private readonly IUserInfoService _userInfoService;

        public CardsController(IUserInfoService userInfoService, ICardQueries cardQueries, IMediator mediator)
        {
            _userInfoService = userInfoService;
            _cardQueries = cardQueries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get user's credit cards.
        /// </summary>
        [HttpGet(Name = nameof(GetCardsAsync))]
        public async Task<IActionResult> GetCardsAsync()
        {
            var customerId = new StripeCustomerId(_userInfoService.StripeCustomerId);

            var cards = await _cardQueries.GetCardsAsync(customerId);

            return cards
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }

        /// <summary>
        ///     Attach a credit card to a user.
        /// </summary>
        [HttpPost(Name = nameof(CreateCardAsync))]
        public async Task<IActionResult> CreateCardAsync([FromBody] CreateCardCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Get user's credit card by card id.
        /// </summary>
        [HttpGet("{cardId}", Name = nameof(GetCardAsync))]
        public async Task<IActionResult> GetCardAsync(StripeCardId cardId)
        {
            var customerId = new StripeCustomerId(_userInfoService.StripeCustomerId);

            var card = await _cardQueries.GetCardAsync(customerId, cardId);

            return card
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("User credit card not found."))
                .Single();
        }

        /// <summary>
        ///     Detach a credit card from a user.
        /// </summary>
        [HttpDelete("{cardId}", Name = nameof(DeleteCardAsync))]
        public async Task<IActionResult> DeleteCardAsync(StripeCardId cardId)
        {
            return await _mediator.SendCommandAsync(new DeleteCardCommand(cardId));
        }

        /// <summary>
        ///     Update the default user credit card.
        /// </summary>
        [HttpPatch("{cardId}/default", Name = nameof(UpdateCardDefaultAsync))]
        public async Task<IActionResult> UpdateCardDefaultAsync(StripeCardId cardId)
        {
            return await _mediator.SendCommandAsync(new UpdateCardDefaultCommand(cardId));
        }
    }
}
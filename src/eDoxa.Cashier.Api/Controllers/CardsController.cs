// Filename: CardsController.cs
// Date Created: 2019-04-30
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
        ///     Find the user's credit cards.
        /// </summary>
        [HttpGet(Name = nameof(FindUserCardsAsync))]
        public async Task<IActionResult> FindUserCardsAsync()
        {
            var customerId = _userInfoService.CustomerId;

            var cards = await _cardQueries.FindUserCardsAsync(CustomerId.Parse(customerId));

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
        ///     Find the user's credit card.
        /// </summary>
        [HttpGet("{cardId}", Name = nameof(FindUserCardAsync))]
        public async Task<IActionResult> FindUserCardAsync(CardId cardId)
        {
            var customerId = _userInfoService.CustomerId;

            var card = await _cardQueries.FindUserCardAsync(CustomerId.Parse(customerId), cardId);

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
        public async Task<IActionResult> DeleteCardAsync(CardId cardId)
        {
            return await _mediator.SendCommandAsync(new DeleteCardCommand(cardId));
        }

        /// <summary>
        ///     Update the default user credit card.
        /// </summary>
        [HttpPatch("{cardId}/default", Name = nameof(UpdateDefaultCardAsync))]
        public async Task<IActionResult> UpdateDefaultCardAsync(CardId cardId)
        {
            return await _mediator.SendCommandAsync(new UpdateCardDefaultCommand(cardId));
        }
    }
}
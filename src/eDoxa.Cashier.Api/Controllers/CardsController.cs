// Filename: UserCardsController.cs
// Date Created: 2019-04-21
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
using eDoxa.Security.Services;

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
        private readonly IMediator _mediator;
        private readonly IUserInfoService _userInfoService;
        private readonly ICardQueries _queries;

        public CardsController(IUserInfoService userInfoService, ICardQueries queries, IMediator mediator)
        {
            _userInfoService = userInfoService;
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find the user's credit cards.
        /// </summary>
        [HttpGet(Name = nameof(FindUserCardsAsync))]
        public async Task<IActionResult> FindUserCardsAsync()
        {
            var customerId = _userInfoService.CustomerId.Select(CustomerId.Parse).SingleOrDefault();

            var cards = await _queries.FindUserCardsAsync(customerId);

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
            var customerId = _userInfoService.CustomerId.Select(CustomerId.Parse).SingleOrDefault();

            var card = await _queries.FindUserCardAsync(customerId, cardId);

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
            var command = new DeleteCardCommand(cardId);

            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Update the default user credit card.
        /// </summary>
        [HttpPatch("{cardId}/default", Name = nameof(UpdateDefaultCardAsync))]
        public async Task<IActionResult> UpdateDefaultCardAsync(CardId cardId)
        {
            var command = new UpdateDefaultCardCommand(cardId);

            return await _mediator.SendCommandAsync(command);
        }
    }
}
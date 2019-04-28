﻿// Filename: UserCardsController.cs
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

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users/{userId}/cards")]
    public class UserCardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICardQueries _queries;

        public UserCardsController(ICardQueries queries, IMediator mediator)
        {
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find the user's credit cards.
        /// </summary>
        [HttpGet(Name = nameof(FindUserCardsAsync))]
        public async Task<IActionResult> FindUserCardsAsync(UserId userId)
        {
            var cards = await _queries.FindUserCardsAsync(userId);

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
        public async Task<IActionResult> CreateCardAsync(
            UserId userId,
            [FromBody] CreateCardCommand command)
        {
            command.UserId = userId;

            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Find the user's credit card.
        /// </summary>
        [HttpGet("{cardId}", Name = nameof(FindUserCardAsync))]
        public async Task<IActionResult> FindUserCardAsync(UserId userId, CardId cardId)
        {
            var card = await _queries.FindUserCardAsync(userId, cardId);

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
        public async Task<IActionResult> DeleteCardAsync(UserId userId, CardId cardId)
        {
            var command = new DeleteCardCommand(userId, cardId);

            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Update the default user credit card.
        /// </summary>
        [HttpPatch("{cardId}/default", Name = nameof(UpdateDefaultCardAsync))]
        public async Task<IActionResult> UpdateDefaultCardAsync(UserId userId, CardId cardId)
        {
            var command = new UpdateDefaultCardCommand(userId, cardId);

            return await _mediator.SendCommandAsync(command);
        }
    }
}
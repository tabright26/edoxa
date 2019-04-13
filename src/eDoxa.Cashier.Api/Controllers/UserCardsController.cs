// Filename: UserCardsController.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Seedwork.Application.Extensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users/{userId}/cards")]
    public class UserCardsController : ControllerBase
    {
        private readonly ILogger<UserCardsController> _logger;
        private readonly ICardQueries _queries;
        private readonly IMediator _mediator;

        public UserCardsController(ILogger<UserCardsController> logger, ICardQueries queries, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        ///     Find the user's credit cards.
        /// </summary>
        [HttpGet(Name = nameof(FindUserCardsAsync))]
        public async Task<IActionResult> FindUserCardsAsync(UserId userId)
        {
            try
            {
                var cards = await _queries.FindUserCardsAsync(userId);

                return this.Ok(cards);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Attach a credit card to a user.
        /// </summary>
        [HttpPost(Name = nameof(CreateCardAsync))]
        public async Task<IActionResult> CreateCardAsync(
            UserId userId,
            [FromBody]
            CreateCardCommand command)
        {
            try
            {
                command.UserId = userId;

                var card = await _mediator.SendCommandAsync(command);

                return this.Created(
                    Url.Link(
                        nameof(this.FindUserCardAsync),
                        new
                        {
                            userId, cardId = card.Id
                        }
                    ),
                    card
                );
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Find the user's credit card.
        /// </summary>
        [HttpGet("{cardId}", Name = nameof(FindUserCardAsync))]
        public async Task<IActionResult> FindUserCardAsync(UserId userId, CardId cardId)
        {
            try
            {
                var card = await _queries.FindUserCardAsync(userId, cardId);

                return this.Ok(card);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Detach a credit card from a user.
        /// </summary>
        [HttpDelete("{cardId}", Name = nameof(DeleteCardAsync))]
        public async Task<IActionResult> DeleteCardAsync(UserId userId, CardId cardId)
        {
            try
            {
                var command = new DeleteCardCommand(userId, cardId);

                await _mediator.SendCommandAsync(command);

                return this.Ok(string.Empty);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Update the default user credit card.
        /// </summary>
        [HttpPatch("{cardId}/default", Name = nameof(UpdateDefaultCardAsync))]
        public async Task<IActionResult> UpdateDefaultCardAsync(UserId userId, CardId cardId)
        {
            try
            {
                var command = new UpdateDefaultCardCommand(userId, cardId);

                var customer = await _mediator.SendCommandAsync(command);

                return this.Ok(customer);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }
    }
}
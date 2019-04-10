// Filename: UserCardsController.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Properties;
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
                _logger.LogError(exception, Resources.CustomerCardsController_Error_FetchCustomerCardsAsync);
            }

            return this.BadRequest(Resources.CustomerCardsController_BadRequest_FetchCustomerCardsAsync);
        }

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
                _logger.LogError(exception, Resources.CustomerCardsController_Error_CreateCustomerCardAsync);
            }

            return this.BadRequest(Resources.CustomerCardsController_BadRequest_CreateCustomerCardAsync);
        }

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
                _logger.LogError(exception, Resources.CustomerCardsController_Error_FindCustomerCardAsync);
            }

            return this.BadRequest(Resources.CustomerCardsController_BadRequest_FindCustomerCardAsync);
        }

        [HttpDelete("{cardId}", Name = nameof(DeleteCardAsync))]
        public async Task<IActionResult> DeleteCardAsync(UserId userId, CardId cardId)
        {
            try
            {
                var command = new DeleteCardCommand(userId, cardId);

                await _mediator.SendCommandAsync(command);

                return this.Ok(Resources.CustomerCardsController_Ok_DeleteCustomerCardAsync);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.CustomerCardsController_Error_DeleteCustomerCardAsync);
            }

            return this.BadRequest(Resources.CustomerCardsController_BadRequest_DeleteCustomerCardAsync);
        }

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
                _logger.LogError(exception, Resources.CustomersController_Error_UpdateCustomerDefaultSourceAsync);
            }

            return this.BadRequest(Resources.CustomersController_BadRequest_UpdateCustomerDefaultSourceAsync);
        }
    }
}
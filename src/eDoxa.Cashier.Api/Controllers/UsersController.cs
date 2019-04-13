﻿// Filename: UsersController.cs
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
using eDoxa.Seedwork.Application.Services;

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
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IAddressQueries _queries;
        private readonly IMediator _mediator;
        private readonly IIdentityParserService _identityParserService;

        public UsersController(ILogger<UsersController> logger, IAddressQueries queries, IMediator mediator, IIdentityParserService identityParserService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _identityParserService = identityParserService ?? throw new ArgumentNullException(nameof(identityParserService));
        }

        /// <summary>
        ///     Find the user's address.
        /// </summary>
        [HttpGet("{userId}/address", Name = nameof(FindUserAddressAsync))]
        public async Task<IActionResult> FindUserAddressAsync(UserId userId)
        {
            try
            {
                var address = await _queries.FindUserAddressAsync(userId);

                return this.Ok(address);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Update the user's address.
        /// </summary>
        [HttpPut("{userId}/address", Name = nameof(UpdateAddressAsync))]
        public async Task<IActionResult> UpdateAddressAsync(
            UserId userId,
            [FromBody]
            UpdateAddressCommand command)
        {
            try
            {
                command.UserId = userId;

                command.Name = "Francis Quenneville"; // TODO: 

                command.Phone = "5147580313"; // TODO: 

                var address = await _mediator.SendCommandAsync(command);

                return this.Ok(address);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }
    }
}
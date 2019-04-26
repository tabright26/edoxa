// Filename: UserTokenAccountController.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
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
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users/{userId}/token-account")]
    public class UserTokenAccountController : ControllerBase
    {
        private readonly ILogger<UserTokenAccountController> _logger;
        private readonly IMediator _mediator;
        private readonly ITokenAccountQueries _queries;

        public UserTokenAccountController(ILogger<UserTokenAccountController> logger, ITokenAccountQueries queries, IMediator mediator)
        {
            _logger = logger;
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find a user's token account.
        /// </summary>
        [HttpGet(Name = nameof(FindTokenAccountAsync))]
        public async Task<IActionResult> FindTokenAccountAsync(UserId userId)
        {
            try
            {
                var account = await _queries.FindAccountAsync(userId);

                if (account == null)
                {
                    return this.NotFound(string.Empty);
                }

                return this.Ok(account);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Buy tokens on a user's account.
        /// </summary>
        [HttpPatch("deposit", Name = nameof(DepositTokensAsync))]
        public async Task<IActionResult> DepositTokensAsync(
            UserId userId,
            [FromBody] DepositTokensCommand command)
        {
            try
            {
                command.UserId = userId;

                var token = await _mediator.SendCommandAsync(command);

                return this.Ok(token);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Find a user's token transactions.
        /// </summary>
        [HttpGet("transactions", Name = nameof(FindTokenTransactionsAsync))]
        public async Task<IActionResult> FindTokenTransactionsAsync(UserId userId)
        {
            try
            {
                var transactions = await _queries.FindTransactionsAsync(userId);

                if (!transactions.Any())
                {
                    return this.NoContent();
                }

                return this.Ok(transactions);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }
    }
}
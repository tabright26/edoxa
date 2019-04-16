// Filename: UserAccountController.cs
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
    [Route("api/users/{userId}/account")]
    public class UserAccountController : ControllerBase
    {
        private readonly ILogger<UserAccountController> _logger;
        private readonly IAccountQueries _queries;
        private readonly IMediator _mediator;

        public UserAccountController(ILogger<UserAccountController> logger, IAccountQueries queries, IMediator mediator)
        {
            _logger = logger;
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find a user's account.
        /// </summary>
        [HttpGet(Name = nameof(FindUserAccountAsync))]
        public async Task<IActionResult> FindUserAccountAsync(UserId userId)
        {
            try
            {
                var coins = await _queries.FindUserAccountAsync(userId);

                if (coins == null)
                {
                    return this.NotFound(string.Empty);
                }

                return this.Ok(coins);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Withdraw funds from a user's account.
        /// </summary>
        [HttpPatch(Name = nameof(WithdrawalAsync))]
        public async Task<IActionResult> WithdrawalAsync(
            UserId userId,
            [FromBody]
            WithdrawalCommand command)
        {
            try
            {
                command.UserId = userId;

                var money = await _mediator.SendCommandAsync(command);

                return this.Ok(money);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }

            return this.BadRequest(string.Empty);
        }

        /// <summary>
        ///     Deposit funds on a user's account.
        /// </summary>
        [HttpPatch("funds", Name = nameof(AddFundsAsync))]
        public async Task<IActionResult> AddFundsAsync(
            UserId userId,
            [FromBody]
            AddFundsCommand command)
        {
            try
            {
                command.UserId = userId;

                var money = await _mediator.SendCommandAsync(command);

                return this.Ok(money);
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
        [HttpPatch("tokens", Name = nameof(BuyTokensAsync))]
        public async Task<IActionResult> BuyTokensAsync(
            UserId userId,
            [FromBody]
            BuyTokensCommand command)
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
    }
}
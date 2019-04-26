// Filename: UserMoneyAccountController.cs
// Date Created: 2019-04-21
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
using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users/{userId}/money-account")]
    public class UserMoneyAccountController : ControllerBase
    {
        private readonly ILogger<UserMoneyAccountController> _logger;
        private readonly IMediator _mediator;
        private readonly IMoneyAccountQueries _queries;

        public UserMoneyAccountController(ILogger<UserMoneyAccountController> logger, IMoneyAccountQueries queries, IMediator mediator)
        {
            _logger = logger;
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find a user's money account.
        /// </summary>
        [HttpGet(Name = nameof(FindMoneyAccountAsync))]
        public async Task<IActionResult> FindMoneyAccountAsync(UserId userId)
        {
            try
            {
                var account = await _queries.FindMoneyAccountAsync(userId);

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
        ///     Withdraw funds from a user's account.
        /// </summary>
        [HttpPatch(Name = nameof(WithdrawAsync))]
        public async Task<IActionResult> WithdrawAsync(
            UserId userId,
            [FromBody] WithdrawalCommand command)
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
            [FromBody] AddFundsCommand command)
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
            [FromBody] BuyTokensCommand command)
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
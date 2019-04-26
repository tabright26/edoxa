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
using Microsoft.EntityFrameworkCore.Internal;
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
        ///     Deposit money on a user's account.
        /// </summary>
        [HttpPatch("deposit", Name = nameof(DepositMoneyAsync))]
        public async Task<IActionResult> DepositMoneyAsync(
            UserId userId,
            [FromBody] DepositMoneyCommand command)
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
        ///     Withdraw money from a user's account.
        /// </summary>
        [HttpPatch("withdraw", Name = nameof(WithdrawMoneyAsync))]
        public async Task<IActionResult> WithdrawMoneyAsync(
            UserId userId,
            [FromBody] WithdrawMoneyCommand command)
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
        ///     Find a user's money transactions.
        /// </summary>
        [HttpGet("transactions", Name = nameof(FindMoneyTransactionsAsync))]
        public async Task<IActionResult> FindMoneyTransactionsAsync(UserId userId)
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
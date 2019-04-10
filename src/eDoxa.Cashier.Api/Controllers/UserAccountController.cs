// Filename: UserAccountController.cs
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
    [Route("api/users/{userId}/account")]
    public class UserAccountController : ControllerBase
    {
        private readonly ILogger<UserAccountController> _logger;
        private readonly IAccountQueries _queries;
        private readonly IMediator _mediator;

        public UserAccountController(ILogger<UserAccountController> logger, IAccountQueries queries, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(Name = nameof(FindUserAccountAsync))]
        public async Task<IActionResult> FindUserAccountAsync(UserId userId)
        {
            try
            {
                var coins = await _queries.FindUserAccountAsync(userId);

                if (coins == null)
                {
                    return this.NotFound(Resources.UserAccountController_NotFound_FindUserWalletAsync);
                }

                return this.Ok(coins);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, Resources.UserAccountController_Error_FindUserWalletAsync);
            }

            return this.BadRequest(Resources.UserAccountController_BadRequest_FindUserWalletAsync);
        }

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
                _logger.LogError(exception, Resources.UserAccountController_Error_WithdrawalAsync);
            }

            return this.BadRequest(Resources.UserAccountController_BadRequest_WithdrawalAsync);
        }

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
                _logger.LogError(exception, Resources.UserAccountController_Error_AddFundsAsync);
            }

            return this.BadRequest(Resources.UserAccountController_BadRequest_AddFundsAsync);
        }

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
                _logger.LogError(exception, Resources.UserAccountController_Error_BuyTokensAsync);
            }

            return this.BadRequest(Resources.UserAccountController_BadRequest_BuyTokensAsync);
        }
    }
}
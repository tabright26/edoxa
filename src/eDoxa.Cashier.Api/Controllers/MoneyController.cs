// Filename: MoneyController.cs
// Date Created: 2019-05-06
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
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/money")]
    public sealed class MoneyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMoneyAccountQueries _queries;
        private readonly IUserInfoService _userInfoService;

        public MoneyController(IUserInfoService userInfoService, IMoneyAccountQueries queries, IMediator mediator)
        {
            _userInfoService = userInfoService;
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find user's money.
        /// </summary>
        [HttpGet(Name = nameof(FindMoneyAccountAsync))]
        public async Task<IActionResult> FindMoneyAccountAsync()
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var account = await _queries.FindAccountAsync(userId);

            return account
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("User money account not found."))
                .Single();
        }



        /// <summary>
        ///     Deposit money.
        /// </summary>
        [HttpPost("deposit", Name = nameof(DepositMoneyAsync))]
        public async Task<IActionResult> DepositMoneyAsync([FromBody] AddFundsCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Find money bundles.
        /// </summary>
        [HttpGet("deposit/bundles", Name = nameof(FindMoneyBundles))]
        public IActionResult FindMoneyBundles()
        {
            return this.Ok(MoneyBundleType.GetAll());
        }

        /// <summary>
        ///     Withdrawal money.
        /// </summary>
        [HttpPost("withdrawal", Name = nameof(WithdrawalMoneyAsync))]
        public async Task<IActionResult> WithdrawalMoneyAsync([FromBody] WithdrawalFundsCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Withdrawal money.
        /// </summary>
        [HttpGet("withdrawal/bundles", Name = nameof(FindWithdrawalMoneyBundles))]
        public IActionResult FindWithdrawalMoneyBundles()
        {
            return this.Ok(WithdrawalMoneyBundleType.GetAll());
        }

        /// <summary>
        ///     Find money transactions.
        /// </summary>
        [HttpGet("transactions", Name = nameof(FindMoneyTransactionsAsync))]
        public async Task<IActionResult> FindMoneyTransactionsAsync()
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var transactions = await _queries.FindTransactionsAsync(userId);

            return transactions
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}
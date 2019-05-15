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
        private readonly IMoneyAccountQueries _moneyAccountQueries;
        private readonly IUserInfoService _userInfoService;

        public MoneyController(IUserInfoService userInfoService, IMoneyAccountQueries moneyAccountQueries, IMediator mediator)
        {
            _userInfoService = userInfoService;
            _moneyAccountQueries = moneyAccountQueries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get money account.
        /// </summary>
        [HttpGet(Name = nameof(GetMoneyAccountAsync))]
        public async Task<IActionResult> GetMoneyAccountAsync()
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var account = await _moneyAccountQueries.GetMoneyAccountAsync(userId);

            return account
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("User money account not found."))
                .Single();
        }

        /// <summary>
        ///     Deposit money.
        /// </summary>
        [HttpPost("deposit", Name = nameof(AddFundsAsync))]
        public async Task<IActionResult> AddFundsAsync([FromBody] AddFundsCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Get money deposit bundles.
        /// </summary>
        [HttpGet("deposit/bundles", Name = nameof(GetMoneyBundles))]
        public IActionResult GetMoneyBundles()
        {
            return this.Ok(MoneyBundleType.GetAll());
        }

        /// <summary>
        ///     Get money transactions.
        /// </summary>
        [HttpGet("transactions", Name = nameof(GetMoneyTransactionsAsync))]
        public async Task<IActionResult> GetMoneyTransactionsAsync()
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var transactions = await _moneyAccountQueries.GetMoneyTransactionsAsync(userId);

            return transactions
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }

        /// <summary>
        ///     Withdrawal money.
        /// </summary>
        [HttpPost("withdrawal", Name = nameof(WithdrawalFundsAsync))]
        public async Task<IActionResult> WithdrawalFundsAsync([FromBody] WithdrawalFundsCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Get money withdrawal bundles.
        /// </summary>
        [HttpGet("withdrawal/bundles", Name = nameof(GetWithdrawalMoneyBundles))]
        public IActionResult GetWithdrawalMoneyBundles()
        {
            return this.Ok(WithdrawalMoneyBundleType.GetAll());
        }
    }
}
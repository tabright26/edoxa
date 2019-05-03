// Filename: UserMoneyAccountController.cs
// Date Created: 2019-04-28
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
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Services;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/account/money")]
    public sealed class AccountMoneyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserInfoService _userInfoService;
        private readonly IMoneyAccountQueries _queries;

        public AccountMoneyController(IUserInfoService userInfoService, IMoneyAccountQueries queries, IMediator mediator)
        {
            _userInfoService = userInfoService;
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find a user's money account.
        /// </summary>
        [HttpGet(Name = nameof(FindMoneyAccountAsync))]
        public async Task<IActionResult> FindMoneyAccountAsync()
        {
            var userId = _userInfoService.Subject.Select(UserId.FromGuid).SingleOrDefault();

            var account = await _queries.FindAccountAsync(userId);

            return account
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("User money account not found."))
                .Single();
        }

        /// <summary>
        ///     Deposit money on a user's account.
        /// </summary>
        [HttpPost("deposit", Name = nameof(DepositMoneyAsync))]
        public async Task<IActionResult> DepositMoneyAsync([FromBody] DepositMoneyCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Withdraw money from a user's account.
        /// </summary>
        [HttpPost("withdraw", Name = nameof(WithdrawMoneyAsync))]
        public async Task<IActionResult> WithdrawMoneyAsync([FromBody] WithdrawMoneyCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Find a user's money transactions.
        /// </summary>
        [HttpGet("transactions", Name = nameof(FindMoneyTransactionsAsync))]
        public async Task<IActionResult> FindMoneyTransactionsAsync()
        {
            var userId = _userInfoService.Subject.Select(UserId.FromGuid).SingleOrDefault();

            var transactions = await _queries.FindTransactionsAsync(userId);

            return transactions
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}
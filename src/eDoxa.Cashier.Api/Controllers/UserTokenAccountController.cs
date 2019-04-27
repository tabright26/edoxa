// Filename: UserTokenAccountController.cs
// Date Created: 2019-04-26
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
using eDoxa.Seedwork.Application.Extensions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/users/{userId}/token-account")]
    public class UserTokenAccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenAccountQueries _queries;

        public UserTokenAccountController(ITokenAccountQueries queries, IMediator mediator)
        {
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find a user's token account.
        /// </summary>
        [HttpGet(Name = nameof(FindTokenAccountAsync))]
        public async Task<IActionResult> FindTokenAccountAsync(UserId userId)
        {
            var account = await _queries.FindAccountAsync(userId);

            return account
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound(string.Empty))
                .Single();
        }

        /// <summary>
        ///     Buy tokens on a user's account.
        /// </summary>
        [HttpPatch("deposit", Name = nameof(DepositTokensAsync))]
        public async Task<IActionResult> DepositTokensAsync(
            UserId userId,
            [FromBody] DepositTokensCommand command)
        {
            command.UserId = userId;

            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Find a user's token transactions.
        /// </summary>
        [HttpGet("transactions", Name = nameof(FindTokenTransactionsAsync))]
        public async Task<IActionResult> FindTokenTransactionsAsync(UserId userId)
        {
            var transactions = await _queries.FindTransactionsAsync(userId);

            return transactions
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}
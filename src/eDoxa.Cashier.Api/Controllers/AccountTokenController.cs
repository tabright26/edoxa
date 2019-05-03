// Filename: UserTokenAccountController.cs
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
using eDoxa.Security;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/account/token")]
    public class AccountTokenController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserInfoService _userInfoService;
        private readonly ITokenAccountQueries _queries;

        public AccountTokenController(IUserInfoService userInfoService, ITokenAccountQueries queries, IMediator mediator)
        {
            _userInfoService = userInfoService;
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Find a user's token account.
        /// </summary>
        [HttpGet(Name = nameof(FindTokenAccountAsync))]
        public async Task<IActionResult> FindTokenAccountAsync()
        {
            var userId = _userInfoService.Subject.Select(UserId.FromGuid).SingleOrDefault();

            var account = await _queries.FindAccountAsync(userId);

            return account
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("User token account not found."))
                .Single();
        }

        /// <summary>
        ///     Buy tokens on a user's account.
        /// </summary>
        [HttpPost("deposit", Name = nameof(DepositTokensAsync))]
        public async Task<IActionResult> DepositTokensAsync([FromBody] DepositTokensCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Find a user's token transactions.
        /// </summary>
        [HttpGet("transactions", Name = nameof(FindTokenTransactionsAsync))]
        public async Task<IActionResult> FindTokenTransactionsAsync()
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
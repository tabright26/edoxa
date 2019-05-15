// Filename: TokenController.cs
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
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
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
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenAccountQueries _tokenAccountQueries;
        private readonly IUserInfoService _userInfoService;

        public TokenController(IUserInfoService userInfoService, ITokenAccountQueries tokenAccountQueries, IMediator mediator)
        {
            _userInfoService = userInfoService;
            _tokenAccountQueries = tokenAccountQueries;
            _mediator = mediator;
        }

        /// <summary>
        ///     Get token account.
        /// </summary>
        [HttpGet(Name = nameof(GetTokenAccountAsync))]
        public async Task<IActionResult> GetTokenAccountAsync()
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var account = await _tokenAccountQueries.GetTokenAccountAsync(userId);

            return account
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("User token account not found."))
                .Single();
        }

        /// <summary>
        ///     Buy tokens.
        /// </summary>
        [HttpPost("deposit", Name = nameof(BuyTokensAsync))]
        public async Task<IActionResult> BuyTokensAsync([FromBody] BuyTokensCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        /// <summary>
        ///     Get token bundles.
        /// </summary>
        [HttpGet("deposit/bundles", Name = nameof(GetTokenBundlesAsync))]
        public IActionResult GetTokenBundlesAsync()
        {
            return this.Ok(TokenBundleType.GetAll());
        }

        /// <summary>
        ///     Get token transactions.
        /// </summary>
        [HttpGet("transactions", Name = nameof(GetTokenTransactionsAsync))]
        public async Task<IActionResult> GetTokenTransactionsAsync()
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var transactions = await _tokenAccountQueries.GetTokenTransactionsAsync(userId);

            return transactions
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NoContent())
                .Single();
        }
    }
}
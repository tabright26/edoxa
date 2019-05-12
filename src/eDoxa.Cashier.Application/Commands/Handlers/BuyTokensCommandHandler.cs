// Filename: BuyTokensCommandHandler.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class BuyTokensCommandHandler : ICommandHandler<BuyTokensCommand, IActionResult>
    {
        private static readonly TokenBundles Bundles = new TokenBundles();
        private readonly ITokenAccountService _tokenAccountService;
        private readonly IUserInfoService _userInfoService;

        public BuyTokensCommandHandler(IUserInfoService userInfoService, ITokenAccountService tokenAccountService)
        {
            _userInfoService = userInfoService;
            _tokenAccountService = tokenAccountService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] BuyTokensCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var customerId = new CustomerId(_userInfoService.CustomerId);

            var bundle = Bundles[command.BundleType];

            var either = await _tokenAccountService.DepositAsync(userId, customerId, bundle, cancellationToken);

            return either.Match<IActionResult>(
                result => new BadRequestObjectResult(result.ErrorMessage),
                transaction => new OkObjectResult(transaction.Status)
            );
        }
    }
}
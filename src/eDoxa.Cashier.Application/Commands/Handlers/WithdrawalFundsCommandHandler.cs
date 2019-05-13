// Filename: WithdrawalFundsCommandHandler.cs
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
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class WithdrawalFundsCommandHandler : ICommandHandler<WithdrawalFundsCommand, IActionResult>
    {
        private static readonly WithdrawalMoneyBundles Bundles = new WithdrawalMoneyBundles();
        private readonly IMoneyAccountService _moneyAccountService;
        private readonly IUserInfoService _userInfoService;

        public WithdrawalFundsCommandHandler(IUserInfoService userInfoService, IMoneyAccountService moneyAccountService)
        {
            _userInfoService = userInfoService;
            _moneyAccountService = moneyAccountService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] WithdrawalFundsCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Parse(_userInfoService.Subject);

            var customerId = new CustomerId(_userInfoService.CustomerId);

            var bundle = Bundles[command.BundleType];

            var either = await _moneyAccountService.TryWithdrawalAsync(userId, customerId, bundle, cancellationToken);

            return either.Match<IActionResult>(
                result => new BadRequestObjectResult(result.ErrorMessage),
                transaction => new OkObjectResult(transaction.Status)
            );
        }
    }
}
// Filename: DepositMoneyCommandHandler.cs
// Date Created: 2019-04-26
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
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand, IActionResult>
    {
        private static readonly MoneyBundles Bundles = new MoneyBundles();

        private readonly IMoneyAccountService _moneyAccountService;
        private readonly IUserInfoService _userInfoService;

        public DepositMoneyCommandHandler(IUserInfoService userInfoService, IMoneyAccountService moneyAccountService)
        {
            _userInfoService = userInfoService;
            _moneyAccountService = moneyAccountService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] DepositMoneyCommand command, CancellationToken cancellationToken)
        {
            var bundle = Bundles[command.BundleType];

            var userId = UserId.Parse(_userInfoService.Subject);

            var customerId = CustomerId.Parse(_userInfoService.CustomerId);

            var transaction = await _moneyAccountService.TransactionAsync(userId, customerId, bundle, cancellationToken);

            return new OkObjectResult(transaction);
        }
    }
}
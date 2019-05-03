﻿// Filename: WithdrawMoneyCommandHandler.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class WithdrawMoneyCommandHandler : ICommandHandler<WithdrawMoneyCommand, IActionResult>
    {
        private readonly IMoneyAccountService _moneyAccountService;
        private readonly IUserInfoService _userInfoService;

        public WithdrawMoneyCommandHandler(IUserInfoService userInfoService, IMoneyAccountService moneyAccountService)
        {
            _userInfoService = userInfoService;
            _moneyAccountService = moneyAccountService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] WithdrawMoneyCommand command, CancellationToken cancellationToken)
        {
            var userId = _userInfoService.Subject.Select(UserId.FromGuid).SingleOrDefault();

            var moneyTransaction = await _moneyAccountService.TryWithdrawAsync(userId, command.Amount, cancellationToken);

            return moneyTransaction
                .Select(transaction => new OkObjectResult(transaction))
                .Cast<IActionResult>()
                .DefaultIfEmpty(new BadRequestResult())
                .Single();
        }
    }
}
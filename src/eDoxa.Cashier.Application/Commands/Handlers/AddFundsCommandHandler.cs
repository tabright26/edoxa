﻿// Filename: AddFundsCommandHandler.cs
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

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class AddFundsCommandHandler : ICommandHandler<AddFundsCommand, IActionResult>
    {
        private static readonly MoneyBundles Bundles = new MoneyBundles();
        private readonly IMapper _mapper;
        private readonly IMoneyAccountService _moneyAccountService;
        private readonly IUserInfoService _userInfoService;

        public AddFundsCommandHandler(IUserInfoService userInfoService, IMoneyAccountService moneyAccountService, IMapper mapper)
        {
            _userInfoService = userInfoService;
            _moneyAccountService = moneyAccountService;
            _mapper = mapper;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] AddFundsCommand command, CancellationToken cancellationToken)
        {
            var bundle = Bundles[command.BundleType];

            var userId = UserId.Parse(_userInfoService.Subject);

            var customerId = new CustomerId(_userInfoService.CustomerId);

            var transaction = await _moneyAccountService.DepositAsync(userId, customerId, bundle, cancellationToken);

            return new OkObjectResult(_mapper.Map<MoneyTransactionDTO>(transaction));
        }
    }
}
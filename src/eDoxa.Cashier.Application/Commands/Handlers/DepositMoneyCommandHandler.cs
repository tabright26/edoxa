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

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand, IActionResult>
    {
        private static readonly MoneyBundles Bundles = new MoneyBundles();
        private readonly IMoneyAccountService _moneyAccountService;

        private readonly IUserRepository _userRepository;

        public DepositMoneyCommandHandler(IUserRepository userRepository, IMoneyAccountService moneyAccountService)
        {
            _userRepository = userRepository;
            _moneyAccountService = moneyAccountService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] DepositMoneyCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(command.UserId);

            var bundle = Bundles[command.BundleType];

            await _moneyAccountService.TransactionAsync(user, bundle, cancellationToken);

            var balance = user.DepositMoney(bundle);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return new OkObjectResult((decimal) balance.Amount);
        }
    }
}
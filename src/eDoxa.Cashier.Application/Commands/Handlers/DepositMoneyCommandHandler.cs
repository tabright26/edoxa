// Filename: AddFundsCommandHandler.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Seedwork.Application.Commands.Handlers;
using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand, decimal>
    {
        private static readonly MoneyBundles Bundles = new MoneyBundles();

        private readonly IUserRepository _userRepository;
        private readonly IMoneyAccountService _moneyAccountService;

        public DepositMoneyCommandHandler(IUserRepository userRepository, IMoneyAccountService moneyAccountService)
        {
            _userRepository = userRepository;
            _moneyAccountService = moneyAccountService;
        }

        public async Task<decimal> Handle([NotNull] DepositMoneyCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(command.UserId);

            var bundle = Bundles[command.BundleType];

            await _moneyAccountService.TransactionAsync(user, bundle, cancellationToken);

            var balance = user.DepositMoney(bundle);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return balance.Amount;
        }
    }
}
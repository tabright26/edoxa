// Filename: BuyTokensCommandHandler.cs
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
    public sealed class DepositTokensCommandHandler : ICommandHandler<DepositTokensCommand, decimal>
    {
        private static readonly TokenBundles Bundles = new TokenBundles();

        private readonly IUserRepository _userRepository;
        private readonly IMoneyAccountService _moneyAccountService;

        public DepositTokensCommandHandler(IUserRepository userRepository, IMoneyAccountService moneyAccountService)
        {
            _userRepository = userRepository;
            _moneyAccountService = moneyAccountService;
        }

        public async Task<decimal> Handle([NotNull] DepositTokensCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(command.UserId);

            var bundle = Bundles[command.BundleType];

            await _moneyAccountService.TransactionAsync(user, bundle, cancellationToken);

            var tokenBalance = user.DepositTokens(bundle);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return tokenBalance.Amount;
        }
    }
}
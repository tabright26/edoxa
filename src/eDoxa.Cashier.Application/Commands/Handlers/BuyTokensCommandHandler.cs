// Filename: BuyTokensCommandHandler.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Services;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class BuyTokensCommandHandler : ICommandHandler<BuyTokensCommand, decimal>
    {
        private static readonly TokenBundles _bundles = new TokenBundles();

        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;

        public BuyTokensCommandHandler(IUserRepository userRepository, IAccountService accountService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<decimal> Handle(BuyTokensCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(command.UserId);

            var bundle = _bundles[command.BundleType];

            await _accountService.TransactionAsync(user.CustomerId, bundle, cancellationToken);

            var tokenBalance = user.BuyTokens(bundle);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return tokenBalance.ToDecimal();
        }
    }
}
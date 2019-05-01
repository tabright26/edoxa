// Filename: DepositTokensCommandHandler.cs
// Date Created: 2019-04-26
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
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Services;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DepositTokensCommandHandler : ICommandHandler<DepositTokensCommand, IActionResult>
    {
        private static readonly TokenBundles Bundles = new TokenBundles();
        private readonly IMoneyAccountService _moneyAccountService;

        private readonly IUserInfoService _userInfoService;
        private readonly IUserRepository _userRepository;

        public DepositTokensCommandHandler(IUserInfoService userInfoService, IUserRepository userRepository, IMoneyAccountService moneyAccountService)
        {
            _userInfoService = userInfoService;
            _userRepository = userRepository;
            _moneyAccountService = moneyAccountService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] DepositTokensCommand command, CancellationToken cancellationToken)
        {
            var userId = _userInfoService.Subject.Select(UserId.FromGuid).SingleOrDefault();

            var user = await _userRepository.FindAsync(userId);

            var bundle = Bundles[command.BundleType];

            await _moneyAccountService.TransactionAsync(user, bundle, cancellationToken);

            var tokenBalance = user.DepositTokens(bundle);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return new OkObjectResult((decimal) tokenBalance.Amount);
        }
    }
}
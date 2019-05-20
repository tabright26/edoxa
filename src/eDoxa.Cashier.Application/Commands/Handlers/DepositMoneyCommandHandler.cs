// Filename: DepositMoneyCommandHandler.cs
// Date Created: 2019-05-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand, Either<ValidationError, TransactionStatus>>
    {
        private static readonly MoneyDepositBundles Bundles = new MoneyDepositBundles();
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IMoneyAccountService _moneyAccountService;
        private readonly IUserRepository _userRepository;

        public DepositMoneyCommandHandler(ICashierHttpContext cashierHttpContext, IMoneyAccountService moneyAccountService, IUserRepository userRepository)
        {
            _cashierHttpContext = cashierHttpContext;
            _moneyAccountService = moneyAccountService;
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, TransactionStatus>> Handle([NotNull] DepositMoneyCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindUserAsNoTrackingAsync(_cashierHttpContext.UserId);

            return await _moneyAccountService.DepositAsync(_cashierHttpContext.UserId, Bundles[command.BundleType], user.CustomerId, cancellationToken);
        }
    }
}

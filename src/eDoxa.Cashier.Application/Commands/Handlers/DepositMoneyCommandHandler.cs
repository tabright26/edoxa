// Filename: DepositMoneyCommandHandler.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand, Either<TransactionStatus>>
    {
        private static readonly MoneyDepositBundles Bundles = new MoneyDepositBundles();
        private readonly ICashierSecurity _cashierSecurity;
        private readonly IMoneyAccountService _moneyAccountService;

        public DepositMoneyCommandHandler(ICashierSecurity cashierSecurity, IMoneyAccountService moneyAccountService)
        {
            _cashierSecurity = cashierSecurity;
            _moneyAccountService = moneyAccountService;
        }

        [ItemNotNull]
        public async Task<Either<TransactionStatus>> Handle([NotNull] DepositMoneyCommand command, CancellationToken cancellationToken)
        {
            var userId = _cashierSecurity.UserId;

            var customerId = _cashierSecurity.StripeCustomerId;

            var bundle = Bundles[command.BundleType];

            return await _moneyAccountService.DepositAsync(customerId, userId, bundle, cancellationToken);
        }
    }
}
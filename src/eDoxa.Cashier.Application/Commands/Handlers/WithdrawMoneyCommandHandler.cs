// Filename: WithdrawMoneyCommandHandler.cs
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
    internal sealed class WithdrawMoneyCommandHandler : ICommandHandler<WithdrawMoneyCommand, Either<TransactionStatus>>
    {
        private static readonly MoneyWithdrawalBundles Bundles = new MoneyWithdrawalBundles();
        private readonly ICashierSecurity _cashierSecurity;
        private readonly IMoneyAccountService _moneyAccountService;

        public WithdrawMoneyCommandHandler(ICashierSecurity cashierSecurity, IMoneyAccountService moneyAccountService)
        {
            _cashierSecurity = cashierSecurity;
            _moneyAccountService = moneyAccountService;
        }

        [ItemNotNull]
        public async Task<Either<TransactionStatus>> Handle([NotNull] WithdrawMoneyCommand command, CancellationToken cancellationToken)
        {
            if (!_cashierSecurity.HasStripeBankAccount())
            {
                return new Failure("A bank account is required to withdrawal.");
            }

            var customerId = _cashierSecurity.StripeAccountId;

            var userId = _cashierSecurity.UserId;

            var bundle = Bundles[command.BundleType];

            return await _moneyAccountService.TryWithdrawalAsync(customerId, userId, bundle, cancellationToken);
        }
    }
}
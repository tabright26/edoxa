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

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
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

        public DepositMoneyCommandHandler(ICashierHttpContext cashierHttpContext, IMoneyAccountService moneyAccountService)
        {
            _cashierHttpContext = cashierHttpContext;
            _moneyAccountService = moneyAccountService;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, TransactionStatus>> Handle([NotNull] DepositMoneyCommand command, CancellationToken cancellationToken)
        {
            return await _moneyAccountService.DepositAsync(_cashierHttpContext.UserId, Bundles[command.BundleType], _cashierHttpContext.StripeCustomerId, cancellationToken);
        }
    }
}
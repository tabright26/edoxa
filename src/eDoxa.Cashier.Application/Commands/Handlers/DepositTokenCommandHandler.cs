// Filename: DepositTokenCommandHandler.cs
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
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DepositTokenCommandHandler : ICommandHandler<DepositTokenCommand, Either<TransactionStatus>>
    {
        private static readonly TokenDepositBundles Bundles = new TokenDepositBundles();
        private readonly ICashierSecurity _cashierSecurity;
        private readonly ITokenAccountService _tokenAccountService;

        public DepositTokenCommandHandler(ICashierSecurity cashierSecurity, ITokenAccountService tokenAccountService)
        {
            _cashierSecurity = cashierSecurity;
            _tokenAccountService = tokenAccountService;
        }

        [ItemNotNull]
        public async Task<Either<TransactionStatus>> Handle([NotNull] DepositTokenCommand command, CancellationToken cancellationToken)
        {
            var userId = _cashierSecurity.UserId;

            var customerId = _cashierSecurity.StripeCustomerId;

            var bundle = Bundles[command.BundleType];

            return await _tokenAccountService.DepositAsync(userId, customerId, bundle, cancellationToken);
        }
    }
}
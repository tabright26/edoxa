// Filename: DepositTokenCommandHandler.cs
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
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DepositTokenCommandHandler : ICommandHandler<DepositTokenCommand, Either<ValidationError, TransactionStatus>>
    {
        private static readonly TokenDepositBundles Bundles = new TokenDepositBundles();
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly ITokenAccountService _tokenAccountService;
        private readonly IUserRepository _userRepository;

        public DepositTokenCommandHandler(ICashierHttpContext cashierHttpContext, ITokenAccountService tokenAccountService, IUserRepository userRepository)
        {
            _cashierHttpContext = cashierHttpContext;
            _tokenAccountService = tokenAccountService;
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, TransactionStatus>> Handle([NotNull] DepositTokenCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindUserAsNoTrackingAsync(_cashierHttpContext.UserId);

            return await _tokenAccountService.DepositAsync(_cashierHttpContext.UserId, Bundles[command.BundleType], user.CustomerId, cancellationToken);
        }
    }
}

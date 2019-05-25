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
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DepositMoneyCommandHandler : ICommandHandler<DepositMoneyCommand, Either<ValidationError, TransactionStatus>>
    {
        private static readonly MoneyDepositBundles Bundles = new MoneyDepositBundles();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMoneyAccountService _moneyAccountService;
        private readonly IUserRepository _userRepository;

        public DepositMoneyCommandHandler(IHttpContextAccessor httpContextAccessor, IMoneyAccountService moneyAccountService, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _moneyAccountService = moneyAccountService;
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, TransactionStatus>> Handle([NotNull] DepositMoneyCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.FindUserAsNoTrackingAsync(userId);

            return await _moneyAccountService.DepositAsync(user.Id, Bundles[command.BundleType], user.CustomerId, cancellationToken);
        }
    }
}

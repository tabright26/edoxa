// Filename: DepositTokensCommandHandler.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DepositTokensCommandHandler : ICommandHandler<DepositTokensCommand, IActionResult>
    {
        private static readonly TokenBundles Bundles = new TokenBundles();
        private readonly ITokenAccountService _tokenAccountService;
        private readonly IUserProfile _userProfile;

        public DepositTokensCommandHandler(IUserProfile userProfile, ITokenAccountService tokenAccountService)
        {
            _userProfile = userProfile;
            _tokenAccountService = tokenAccountService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] DepositTokensCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Parse(_userProfile.Subject);

            var customerId = CustomerId.Parse(_userProfile.CustomerId);

            var bundle = Bundles[command.BundleType];

            var transaction = await _tokenAccountService.TransactionAsync(userId, customerId, bundle, cancellationToken);

            return new OkObjectResult(transaction);
        }
    }
}
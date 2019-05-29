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
using eDoxa.Cashier.Services.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Security.Extensions;

using FluentValidation.Results;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class DepositTokenCommandHandler : ICommandHandler<DepositTokenCommand, Either<ValidationResult, TransactionStatus>>
    {
        private static readonly TokenDepositBundles Bundles = new TokenDepositBundles();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenAccountService _tokenAccountService;
        private readonly IUserRepository _userRepository;

        public DepositTokenCommandHandler(IHttpContextAccessor httpContextAccessor, ITokenAccountService tokenAccountService, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenAccountService = tokenAccountService;
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<Either<ValidationResult, TransactionStatus>> Handle([NotNull] DepositTokenCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsNoTrackingAsync(userId);

            return await _tokenAccountService.DepositAsync(user.Id, Bundles[command.BundleType], user.CustomerId, cancellationToken);
        }
    }
}

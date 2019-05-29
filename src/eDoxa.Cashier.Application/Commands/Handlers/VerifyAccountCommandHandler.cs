// Filename: VerifyAccountCommandHandler.cs
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

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Services.Stripe.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Security.Extensions;

using FluentValidation.Results;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class VerifyAccountCommandHandler : ICommandHandler<VerifyAccountCommand, Either<ValidationResult, CommandResult>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public VerifyAccountCommandHandler(IStripeService stripeService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _stripeService = stripeService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<Either<ValidationResult, CommandResult>> Handle([NotNull] VerifyAccountCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsNoTrackingAsync(userId);

            await _stripeService.VerifyAccountAsync(
                user.AccountId,
                command.Line1,
                command.Line2,
                command.City,
                command.State,
                command.PostalCode,
                cancellationToken
            );

            return new CommandResult("Stripe account verified.");
        }
    }
}

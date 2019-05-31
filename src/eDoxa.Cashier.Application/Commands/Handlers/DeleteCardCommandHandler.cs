// Filename: DeleteCardCommandHandler.cs
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
using eDoxa.Cashier.Services.Extensions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Security.Extensions;
using eDoxa.Stripe.Abstractions;

using FluentValidation.Results;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class DeleteCardCommandHandler : ICommandHandler<DeleteCardCommand, Either<ValidationResult, CommandResult>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public DeleteCardCommandHandler(IHttpContextAccessor httpContextAccessor, IStripeService stripeService, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _stripeService = stripeService;
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<Either<ValidationResult, CommandResult>> Handle([NotNull] DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsNoTrackingAsync(userId);

            await _stripeService.DeleteCardAsync(user.GetCustomerId(), command.StripeCardId, cancellationToken);

            return new CommandResult("The card has been removed.");
        }
    }
}

﻿// Filename: CreateCardCommandHandler.cs
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
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class CreateCardCommandHandler : ICommandHandler<CreateCardCommand, Either<ValidationError, CommandResult>>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public CreateCardCommandHandler(ICashierHttpContext cashierHttpContext, IStripeService stripeService, IUserRepository userRepository)
        {
            _cashierHttpContext = cashierHttpContext;
            _stripeService = stripeService;
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, CommandResult>> Handle([NotNull] CreateCardCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindUserAsNoTrackingAsync(_cashierHttpContext.UserId);

            await _stripeService.CreateCardAsync(user.CustomerId, command.SourceToken, cancellationToken);

            return new CommandResult("The card has been added.");
        }
    }
}

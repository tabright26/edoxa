// Filename: CreateUserCommandHandler.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class CreateUserCommandHandler : AsyncCommandHandler<CreateUserCommand>
    {
        private readonly IStripeService _stripeService;

        public CreateUserCommandHandler(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        protected override async Task Handle([NotNull] CreateUserCommand command, CancellationToken cancellationToken)
        {
            await _stripeService.CreateCustomerAsync(command.UserId, command.Email, cancellationToken);
        }
    }
}
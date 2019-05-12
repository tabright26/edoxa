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

using eDoxa.Cashier.Application.IntegrationEvents;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security;
using eDoxa.ServiceBus;

using FluentValidation;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class CreateUserCommandHandler : AsyncCommandHandler<CreateUserCommand>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly IStripeService _stripeService;

        public CreateUserCommandHandler(IStripeService stripeService, IIntegrationEventService integrationEventService)
        {
            _stripeService = stripeService;
            _integrationEventService = integrationEventService;
        }

        protected override async Task Handle([NotNull] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var either = await _stripeService.CreateCustomerAsync(command.UserId, command.Email, cancellationToken);

            await either.Match(
                result => throw new ValidationException(result.ErrorMessage),
                async customer => await _integrationEventService.PublishAsync(
                    new UserClaimAddedIntegrationEvent(
                        command.UserId.ToGuid(),
                        CustomClaimTypes.CustomerId,
                        customer.Id.ToString()
                    )
                )
            );
        }
    }
}
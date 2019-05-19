// Filename: InitializeServiceCommandHandler.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class InitializeServiceCommandHandler : AsyncCommandHandler<InitializeServiceCommand>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly IMoneyAccountService _moneyAccountService;
        private readonly IStripeService _stripeService;
        private readonly ITokenAccountService _tokenAccountService;

        public InitializeServiceCommandHandler(
            IStripeService stripeService,
            IIntegrationEventService integrationEventService,
            IMoneyAccountService moneyAccountService,
            ITokenAccountService tokenAccountService
        )
        {
            _stripeService = stripeService;
            _integrationEventService = integrationEventService;
            _moneyAccountService = moneyAccountService;
            _tokenAccountService = tokenAccountService;
        }

        protected override async Task Handle([NotNull] InitializeServiceCommand command, CancellationToken cancellationToken)
        {
            await _moneyAccountService.CreateAccount(command.UserId);

            await _tokenAccountService.CreateAccount(command.UserId);

            var accountId = await _stripeService.CreateAccountAsync(
                command.UserId,
                command.Email,
                command.FirstName,
                command.LastName,
                command.Year,
                command.Month,
                command.Day,
                cancellationToken
            );

            var customerId = await _stripeService.CreateCustomerAsync(command.UserId, accountId, command.Email, cancellationToken);

            await this.PropagateClaimAsync(command.UserId, accountId, customerId);
        }

        private async Task PropagateClaimAsync(UserId userId, StripeAccountId accountId, StripeCustomerId customerId)
        {
            await _integrationEventService.PublishAsync(
                new UserClaimAddedIntegrationEvent(
                    userId.ToGuid(),
                    new Dictionary<string, string>
                    {
                        [CustomClaimTypes.StripeAccountId] = accountId.ToString(),
                        [CustomClaimTypes.StripeCustomerId] = customerId.ToString()
                    }
                )
            );
        }
    }
}

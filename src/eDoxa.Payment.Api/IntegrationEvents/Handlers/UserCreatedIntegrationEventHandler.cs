// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.IntegrationEvents;
using eDoxa.Payment.Api.Providers.Stripe.Abstractions;
using eDoxa.Seedwork.Security.Constants;

using Microsoft.Extensions.Logging;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly ILogger<UserCreatedIntegrationEventHandler> _logger;
        private readonly IEventBusService _eventBusService;
        private readonly IStripeService _stripeService;

        public UserCreatedIntegrationEventHandler(
            ILogger<UserCreatedIntegrationEventHandler> logger,
            IEventBusService eventBusService,
            IStripeService stripeService
        )
        {
            _logger = logger;
            _eventBusService = eventBusService;
            _stripeService = stripeService;
        }

        public async Task Handle(UserCreatedIntegrationEvent integrationEvent)
        {
            _logger.LogInformation($"Handling {nameof(UserCreatedIntegrationEventHandler)}...");

            var connectAccountId = await _stripeService.CreateAccountAsync(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName,
                integrationEvent.Year,
                integrationEvent.Month,
                integrationEvent.Day
            );

            var customerId = await _stripeService.CreateCustomerAsync(integrationEvent.UserId, connectAccountId, integrationEvent.Email);

            _logger.LogInformation($"Handled {nameof(UserCreatedIntegrationEventHandler)}.");

            _eventBusService.Publish(
                new UserClaimsAddedIntegrationEvent
                {
                    UserId = integrationEvent.UserId,
                    Claims = new Dictionary<string, string>
                    {
                        [CustomClaimTypes.StripeConnectAccountId] = connectAccountId,
                        [CustomClaimTypes.StripeCustomerId] = customerId
                    }
                }
            );

            _logger.LogInformation($"Published {nameof(UserClaimsAddedIntegrationEvent)}.");
        }
    }
}

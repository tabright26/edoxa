// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Payment.Api.Application.Stripe.Services.Abstractions;
using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger; // FRANCIS: Add logs.

        public UserCreatedIntegrationEventHandler(
            IStripeCustomerService stripeCustomerService,
            IServiceBusPublisher serviceBusPublisher,
            ILogger<UserCreatedIntegrationEventHandler> logger
        )
        {
            _stripeCustomerService = stripeCustomerService;
            _serviceBusPublisher = serviceBusPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(UserCreatedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            var customer = await _stripeCustomerService.CreateCustomerAsync(userId, integrationEvent.Email.Address);

            await _serviceBusPublisher.PublishUserStripeCustomerCreatedIntegrationEventAsync(userId, customer);
        }
    }
}

// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeAccountService _stripeAccountService;
        private readonly IStripeReferenceService _stripeReferenceService;

        public UserCreatedIntegrationEventHandler(
            IStripeCustomerService stripeCustomerService,
            IStripeAccountService stripeAccountService,
            IStripeReferenceService stripeReferenceService
        )
        {
            _stripeCustomerService = stripeCustomerService;
            _stripeAccountService = stripeAccountService;
            _stripeReferenceService = stripeReferenceService;
        }

        // TODO: Logger is missing.
        public async Task HandleAsync(UserCreatedIntegrationEvent integrationEvent)
        {
            var customerId = await _stripeCustomerService.CreateCustomerAsync(integrationEvent.UserId, integrationEvent.Email);

            var accountId = await _stripeAccountService.CreateAccountAsync(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.Country,
                customerId);

            await _stripeReferenceService.CreateReferenceAsync(integrationEvent.UserId, customerId, accountId);
        }
    }
}

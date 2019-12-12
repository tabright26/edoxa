// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Misc;
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
            var userId = UserId.Parse(integrationEvent.UserId);

            var customerId = await _stripeCustomerService.CreateCustomerAsync(userId, integrationEvent.Email);

            var accountId = await _stripeAccountService.CreateAccountAsync(
                userId,
                integrationEvent.Email,
                Country.FromValue((int) integrationEvent.Country),
                customerId);

            await _stripeReferenceService.CreateReferenceAsync(userId, customerId, accountId);
        }
    }
}

// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-10-08
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
        private readonly IStripeAccountService _stripeAccount;
        private readonly IStripeService _stripeService;

        public UserCreatedIntegrationEventHandler(
            IStripeCustomerService stripeCustomerService,
            IStripeAccountService stripeAccount,
            IStripeService stripeService
        )
        {
            _stripeCustomerService = stripeCustomerService;
            _stripeAccount = stripeAccount;
            _stripeService = stripeService;
        }
        
        // TODO: Logger is missing.
        public async Task HandleAsync(UserCreatedIntegrationEvent integrationEvent)
        {
            var customerId = await _stripeCustomerService.CreateCustomerAsync(integrationEvent.UserId, integrationEvent.Email);

            var accountId = await _stripeAccount.CreateAccountAsync(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.Country);

            await _stripeService.CreateReferenceAsync(integrationEvent.UserId, customerId, accountId);
        }
    }
}

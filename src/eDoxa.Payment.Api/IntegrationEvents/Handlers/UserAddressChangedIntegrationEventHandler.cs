// Filename: UserAddressChangedIntegrationEventHandler.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.ServiceBus.Abstractions;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserAddressChangedIntegrationEventHandler : IIntegrationEventHandler<UserAddressChangedIntegrationEvent>
    {
        private readonly IStripeAccountService _stripeAccountService;

        public UserAddressChangedIntegrationEventHandler(IStripeAccountService stripeAccountService)
        {
            _stripeAccountService = stripeAccountService;
        }

        public async Task HandleAsync(UserAddressChangedIntegrationEvent integrationEvent)
        {
            var accountId = await _stripeAccountService.GetAccountIdAsync(integrationEvent.UserId);

            await _stripeAccountService.UpdateIndividualAsync(
                accountId,
                new PersonUpdateOptions
                {
                    Address = new AddressOptions
                    {
                        Line1 = integrationEvent.Line1,
                        Line2 = integrationEvent.Line2,
                        State = integrationEvent.State,
                        City = integrationEvent.City,
                        PostalCode = integrationEvent.PostalCode
                    }
                });
        }
    }
}

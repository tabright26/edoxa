// Filename: UserPhoneChangedIntegrationEventHandler.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserPhoneChangedIntegrationEventHandler : IIntegrationEventHandler<UserPhoneChangedIntegrationEvent>
    {
        private readonly IStripeAccountService _stripeAccountService;

        public UserPhoneChangedIntegrationEventHandler(IStripeAccountService stripeAccountService)
        {
            _stripeAccountService = stripeAccountService;
        }

        public async Task HandleAsync(UserPhoneChangedIntegrationEvent integrationEvent)
        {
            var accountId = await _stripeAccountService.GetAccountIdAsync(UserId.Parse(integrationEvent.UserId));

            await _stripeAccountService.UpdateIndividualAsync(
                accountId,
                new PersonUpdateOptions
                {
                    Phone = integrationEvent.PhoneNumber
                });
        }
    }
}

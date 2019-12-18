// Filename: UserAddressChangedIntegrationEventHandler.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserAddressChangedIntegrationEventHandler : IIntegrationEventHandler<UserAddressChangedIntegrationEvent>
    {
        private readonly IStripeService _stripeService;
        private readonly IStripeAccountService _stripeAccountService;
        private readonly ILogger _logger;

        public UserAddressChangedIntegrationEventHandler(
            IStripeService stripeService,
            IStripeAccountService stripeAccountService,
            ILogger<UserAddressChangedIntegrationEventHandler> logger
        )
        {
            _stripeService = stripeService;
            _stripeAccountService = stripeAccountService;
            _logger = logger;
        }

        public async Task HandleAsync(UserAddressChangedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            if (await _stripeService.UserExistsAsync(userId))
            {
                var accountId = await _stripeAccountService.GetAccountIdAsync(userId);

                var result = await _stripeAccountService.UpdateIndividualAsync(
                    accountId,
                    new PersonUpdateOptions
                    {
                        Address = new AddressOptions
                        {
                            Line1 = integrationEvent.Address.Line1,
                            Line2 = integrationEvent.Address.Line2,
                            State = integrationEvent.Address.State,
                            City = integrationEvent.Address.City,
                            PostalCode = integrationEvent.Address.PostalCode
                        }
                    });

                if (result.IsValid)
                {
                    _logger.LogInformation(""); // FRANCIS: TODO.
                }
                else
                {
                    _logger.LogCritical(""); // FRANCIS: TODO.
                }
            }
            else
            {
                _logger.LogCritical(""); // FRANCIS: TODO.
            }
        }
    }
}

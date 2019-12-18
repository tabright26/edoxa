// Filename: UserEmailChangedIntegrationEventHandler.cs
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
    public sealed class UserEmailChangedIntegrationEventHandler : IIntegrationEventHandler<UserEmailChangedIntegrationEvent>
    {
        private readonly IStripeService _stripeService;
        private readonly IStripeAccountService _stripeAccountService;
        private readonly ILogger _logger;

        public UserEmailChangedIntegrationEventHandler(
            IStripeService stripeService,
            IStripeAccountService stripeAccountService,
            ILogger<UserEmailChangedIntegrationEventHandler> logger
        )
        {
            _stripeService = stripeService;
            _stripeAccountService = stripeAccountService;
            _logger = logger;
        }

        public async Task HandleAsync(UserEmailChangedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            if (await _stripeService.UserExistsAsync(userId))
            {
                var accountId = await _stripeAccountService.GetAccountIdAsync(userId);

                var result = await _stripeAccountService.UpdateIndividualAsync(
                    accountId,
                    new PersonUpdateOptions
                    {
                        Email = integrationEvent.Email.Address
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

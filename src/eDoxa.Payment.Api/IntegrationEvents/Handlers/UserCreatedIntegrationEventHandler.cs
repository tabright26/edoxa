// Filename: UserCreatedIntegrationEventHandler.cs
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

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeAccountService _stripeAccountService;
        private readonly IStripeService _stripeService;
        private readonly ILogger _logger;

        public UserCreatedIntegrationEventHandler(
            IStripeCustomerService stripeCustomerService,
            IStripeAccountService stripeAccountService,
            IStripeService stripeService,
            ILogger<UserCreatedIntegrationEventHandler> logger
        )
        {
            _stripeCustomerService = stripeCustomerService;
            _stripeAccountService = stripeAccountService;
            _stripeService = stripeService;
            _logger = logger;
        }

        public async Task HandleAsync(UserCreatedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            if (!await _stripeService.UserExistsAsync(userId))
            {
                var customerId = await _stripeCustomerService.CreateCustomerAsync(userId, integrationEvent.Email.Address);

                var accountId = await _stripeAccountService.CreateAccountAsync(
                    userId,
                    integrationEvent.Email.Address,
                    integrationEvent.Country.ToEnumeration<Country>(),
                    integrationEvent.Ip,
                    customerId,
                    integrationEvent.Dob.Day,
                    integrationEvent.Dob.Month,
                    integrationEvent.Dob.Year);

                var result = await _stripeService.CreateAsync(userId, customerId, accountId);

                if (result.IsValid)
                {
                    _logger.LogError(""); // FRANCIS: TODO.
                }
                else
                {
                    _logger.LogCritical(""); // FRANCIS: TODO.
                }
            }
            else
            {
                _logger.LogWarning(""); // FRANCIS: TODO.
            }
        }
    }
}

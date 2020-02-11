// Filename: UserStripeCustomerCreatedIntegrationEventHandler.cs
// Date Created: 2020-02-05
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserStripeCustomerCreatedIntegrationEventHandler : IIntegrationEventHandler<UserStripeCustomerCreatedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger; // FRANCIS: ADD LOGS.

        public UserStripeCustomerCreatedIntegrationEventHandler(IUserService userService, ILogger<UserStripeCustomerCreatedIntegrationEventHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(UserStripeCustomerCreatedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId);

            await _userService.AddClaimAsync(user, new Claim(CustomClaimTypes.StripeCustomer, integrationEvent.Customer));
        }
    }
}

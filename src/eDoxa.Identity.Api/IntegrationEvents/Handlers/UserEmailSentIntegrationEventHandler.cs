// Filename: UserEmailSentIntegrationEventHandler.cs
// Date Created: 2019-10-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Api.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserEmailSentIntegrationEventHandler : IIntegrationEventHandler<UserEmailSentIntegrationEvent>
    {
        private readonly IUserManager _userManager;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public UserEmailSentIntegrationEventHandler(IUserManager userManager, IServiceBusPublisher serviceBusPublisher)
        {
            _userManager = userManager;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task HandleAsync(UserEmailSentIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            var email = await _userManager.GetEmailAsync(user);

            await _serviceBusPublisher.PublishEmailSentIntegrationEventAsync(
                integrationEvent.UserId,
                email,
                integrationEvent.Subject,
                integrationEvent.HtmlMessage);
        }
    }
}

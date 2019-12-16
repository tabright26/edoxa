// Filename: UserEmailSentIntegrationEventHandler.cs
// Date Created: 2019-10-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserEmailSentIntegrationEventHandler : IIntegrationEventHandler<UserEmailSentIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public UserEmailSentIntegrationEventHandler(IUserService userService, IServiceBusPublisher serviceBusPublisher)
        {
            _userService = userService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task HandleAsync(UserEmailSentIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId);

            var email = await _userService.GetEmailAsync(user);

            await _serviceBusPublisher.PublishEmailSentIntegrationEventAsync(
                UserId.Parse(integrationEvent.UserId),
                email,
                integrationEvent.Subject,
                integrationEvent.HtmlMessage);
        }
    }
}

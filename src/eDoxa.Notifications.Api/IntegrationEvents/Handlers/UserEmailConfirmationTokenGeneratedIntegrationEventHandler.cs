// Filename: UserEmailConfirmationTokenGeneratedIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Notifications.Api.Application;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class
        UserEmailConfirmationTokenGeneratedIntegrationEventHandler : IIntegrationEventHandler<UserEmailConfirmationTokenGeneratedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly IRedirectService _redirectService;

        public UserEmailConfirmationTokenGeneratedIntegrationEventHandler(IUserService userService, IRedirectService redirectService)
        {
            _userService = userService;
            _redirectService = redirectService;
        }

        public async Task HandleAsync(UserEmailConfirmationTokenGeneratedIntegrationEvent integrationEvent)
        {
            var tokenUrl = $"{_redirectService.RedirectToWebSpa("/email/confirm")}?userId={integrationEvent.UserId}&code={integrationEvent.Code}";

            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                SendGridTemplates.UserEmailConfirmationTokenGenerated,
                new
                {
                    tokenUrl
                });
        }
    }
}

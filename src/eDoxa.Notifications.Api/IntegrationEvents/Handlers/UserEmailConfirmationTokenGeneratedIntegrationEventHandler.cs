// Filename: UserEmailConfirmationTokenGeneratedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
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
            var callbackUrl = $"{_redirectService.RedirectToWebSpa("/email/confirm")}?userId={integrationEvent.UserId}&code={integrationEvent.Code}";

            var href = WebUtility.UrlEncode(callbackUrl);

            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                "Confirm your email",
                $"Please confirm your account by <a href=\"{href}\">clicking here</a>.");
        }
    }
}

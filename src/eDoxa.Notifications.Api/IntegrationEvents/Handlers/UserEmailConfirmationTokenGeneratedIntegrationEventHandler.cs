// Filename: UserEmailConfirmationTokenGeneratedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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

        public UserEmailConfirmationTokenGeneratedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserEmailConfirmationTokenGeneratedIntegrationEvent integrationEvent)
        {
            //var callbackUrl = $"{_redirectService.RedirectToWebSpa("/email/confirm")}?userId={user.Id}&code={code}";

            //await _serviceBusPublisher.PublishEmailSentIntegrationEventAsync(
            //    UserId.FromGuid(user.Id),
            //    Input.Email,
            //    "Confirm your email",
            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                nameof(UserCreatedIntegrationEvent),
                nameof(UserCreatedIntegrationEvent));
        }
    }
}

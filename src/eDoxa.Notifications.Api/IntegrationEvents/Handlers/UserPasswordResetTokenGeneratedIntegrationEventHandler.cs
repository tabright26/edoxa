// Filename: UserPasswordResetTokenGeneratedIntegrationEventHandler.cs
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
    public sealed class UserPasswordResetTokenGeneratedIntegrationEventHandler : IIntegrationEventHandler<UserPasswordResetTokenGeneratedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public UserPasswordResetTokenGeneratedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserPasswordResetTokenGeneratedIntegrationEvent integrationEvent)
        {
            //var callbackUrl = $"{_redirectService.RedirectToWebSpa("/password/reset")}?code={HttpUtility.UrlEncode(code)}";

            //await _serviceBusPublisher.PublishEmailSentIntegrationEventAsync(
            //    UserId.FromGuid(user.Id),
            //    request.Email,
            //    "Reset Password",
            //    $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");

            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                nameof(UserPasswordResetTokenGeneratedIntegrationEvent),
                nameof(UserPasswordResetTokenGeneratedIntegrationEvent));
        }
    }
}

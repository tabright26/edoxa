// Filename: UserPasswordResetTokenGeneratedIntegrationEventHandler.cs
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
    public sealed class UserPasswordResetTokenGeneratedIntegrationEventHandler : IIntegrationEventHandler<UserPasswordResetTokenGeneratedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly IRedirectService _redirectService;

        public UserPasswordResetTokenGeneratedIntegrationEventHandler(IUserService userService, IRedirectService redirectService)
        {
            _userService = userService;
            _redirectService = redirectService;
        }

        public async Task HandleAsync(UserPasswordResetTokenGeneratedIntegrationEvent integrationEvent)
        {
            //var callbackUrl = $"{_redirectService.RedirectToWebSpa("/password/reset")}?code={integrationEvent.Code}";

            //var href = WebUtility.UrlEncode(callbackUrl);

            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                SendGridTemplates.UserPasswordResetTokenGenerated,
                integrationEvent);

            // $"Please reset your password by <a href=\"{href}\"'>clicking here</a>."
        }
    }
}

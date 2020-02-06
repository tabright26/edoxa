// Filename: UserPasswordResetTokenGeneratedIntegrationEventHandler.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Sendgrid;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Options;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class UserPasswordResetTokenGeneratedIntegrationEventHandler : IIntegrationEventHandler<UserPasswordResetTokenGeneratedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly IRedirectService _redirectService;
        private readonly IOptions<SendgridOptions> _options;

        public UserPasswordResetTokenGeneratedIntegrationEventHandler(
            IUserService userService,
            IRedirectService redirectService,
            IOptionsSnapshot<SendgridOptions> options
        )
        {
            _userService = userService;
            _redirectService = redirectService;
            _options = options;
        }

        private SendgridOptions Options => _options.Value;

        public async Task HandleAsync(UserPasswordResetTokenGeneratedIntegrationEvent integrationEvent)
        {
            var tokenUrl = $"{_redirectService.RedirectToWebSpa("/password/reset")}?code={integrationEvent.Code}";

            await _userService.SendEmailAsync(
                integrationEvent.UserId.ParseEntityId<UserId>(),
                Options.Templates.UserPasswordResetTokenGenerated,
                new
                {
                    tokenUrl
                });
        }
    }
}

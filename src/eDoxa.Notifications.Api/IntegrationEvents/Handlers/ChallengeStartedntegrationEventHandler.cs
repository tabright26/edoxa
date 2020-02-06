// Filename: ChallengeStartedntegrationEventHandler.cs
// Date Created: 2020-01-29
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Sendgrid;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Options;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeStartedntegrationEventHandler : IIntegrationEventHandler<ChallengeStartedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly IOptions<SendgridOptions> _options;

        public ChallengeStartedntegrationEventHandler(IUserService userService, IOptionsSnapshot<SendgridOptions> options)
        {
            _userService = userService;
            _options = options;
        }

        private SendgridOptions Options => _options.Value;

        public async Task HandleAsync(ChallengeStartedIntegrationEvent integrationEvent)
        {
            foreach (var participant in integrationEvent.Challenge.Participants)
            {
                await _userService.SendEmailAsync(participant.UserId.ParseEntityId<UserId>(), Options.Templates.ChallengeStarted, integrationEvent);
            }
        }
    }
}

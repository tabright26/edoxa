// Filename: ChallengeStartedntegrationEventHandler.cs
// Date Created: 2020-01-29
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Notifications.Api.Application;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeStartedntegrationEventHandler : IIntegrationEventHandler<ChallengeStartedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public ChallengeStartedntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ChallengeStartedIntegrationEvent integrationEvent)
        {
            foreach (var participant in integrationEvent.Challenge.Participants)
            {
                await _userService.SendEmailAsync(participant.UserId.ParseEntityId<UserId>(), SendGridTemplates.ChallengeStarted, integrationEvent);
            }
        }
    }
}

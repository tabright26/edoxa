// Filename: ChallengeParticipantRegisteredIntegrationEventHandler.cs
// Date Created: 2019-12-26
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
    public sealed class ChallengeParticipantRegisteredIntegrationEventHandler : IIntegrationEventHandler<ChallengeParticipantRegisteredIntegrationEvent>
    {
        private readonly IUserService _userService;

        public ChallengeParticipantRegisteredIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ChallengeParticipantRegisteredIntegrationEvent integrationEvent)
        {
            await _userService.SendEmailAsync(
                integrationEvent.Participant.UserId.ParseEntityId<UserId>(),
                SendGridTemplates.ChallengeParticipantRegistered,
                integrationEvent);
        }
    }
}

// Filename: ChallengeClosedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class ChallengeClosedIntegrationEventHandler : IIntegrationEventHandler<ChallengeClosedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public ChallengeClosedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(ChallengeClosedIntegrationEvent integrationEvent)
        {
            foreach (var (userId, _) in integrationEvent.PayoutPrizes)
            {
                await _userService.SendEmailAsync(
                    userId.ParseEntityId<UserId>(),
                    nameof(ChallengeClosedIntegrationEvent),
                    nameof(ChallengeClosedIntegrationEvent));
            }
        }
    }
}

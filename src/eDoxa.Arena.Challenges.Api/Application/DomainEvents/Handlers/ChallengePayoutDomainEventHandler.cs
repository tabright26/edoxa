// Filename: ChallengePayoutDomainEventHandler.cs
// Date Created: 2019-06-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Arena.Challenges.Api.Application.DomainEvents.Handlers
{
    //public sealed class ChallengePayoutDomainEventHandler : IDomainEventHandler<ChallengePayoutDomainEvent>
    //{
    //    private readonly IIntegrationEventService _integrationEventService;

    //    public ChallengePayoutDomainEventHandler(IIntegrationEventService integrationEventService)
    //    {
    //        _integrationEventService = integrationEventService;
    //    }

    //    public async Task Handle([NotNull] ChallengePayoutDomainEvent domainEvent, CancellationToken cancellationToken)
    //    {
    //        await _integrationEventService.PublishAsync(
    //            new ChallengePayoutIntegrationEvent(
    //                domainEvent.ChallengeId.ToGuid(),
    //                domainEvent.ParticipantPrizes.ToDictionary(
    //                    userPrize => userPrize.Key.ToGuid(),
    //                    userPrize => userPrize.Value != null ? (decimal?) userPrize.Value : null
    //                )
    //            )
    //        );
    //    }
    //}
}

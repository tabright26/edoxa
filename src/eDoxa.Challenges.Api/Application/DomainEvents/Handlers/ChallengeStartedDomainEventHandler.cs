// Filename: ChallengeStartedDomainEventHandler.cs
// Date Created: 2019-11-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Api.Application.DomainEvents.Handlers
{
    public sealed class ChallengeStartedDomainEventHandler : IDomainEventHandler<ChallengeStartedDomainEvent>
    {
        public Task Handle(ChallengeStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

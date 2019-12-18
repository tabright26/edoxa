// Filename: ChallengeCreatedDomainEventHandler.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Api.Application.DomainEvents.Handlers
{
    public sealed class ChallengeCreatedDomainEventHandler : IDomainEventHandler<ChallengeCreatedDomainEvent>
    {
        public Task Handle(ChallengeCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

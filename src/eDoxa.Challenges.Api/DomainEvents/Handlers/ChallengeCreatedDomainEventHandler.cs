// Filename: ChallengeCreatedDomainEventHandler.cs
// Date Created: 2019-11-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Challenges.Api.DomainEvents.Handlers
{
    public sealed class ChallengeCreatedDomainEventHandler : IDomainEventHandler<ChallengeCreatedDomainEvent>
    {
        public async Task Handle(ChallengeCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
        }
    }
}

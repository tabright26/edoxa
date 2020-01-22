// Filename: PromotionRedeemedDomainEventHandler.cs
// Date Created: 2020-01-21
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Api.Application.DomainEvents.Handlers
{
    public sealed class PromotionRedeemedDomainEventHandler : IDomainEventHandler<PromotionRedeemedDomainEvent>
    {
        public async Task Handle(PromotionRedeemedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }
    }
}

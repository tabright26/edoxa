// Filename: UserCreatedDomainEventHandler.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.IntegrationEvents;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate.DomainEvents;
using eDoxa.Security;
using eDoxa.Seedwork.Application.DomainEventHandlers;
using eDoxa.ServiceBus;
using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.DomainEventHandlers
{
    public sealed class UserCreatedDomainEventHandler : IDomainEventHandler<UserCreatedDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;

        public UserCreatedDomainEventHandler(IIntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService;
        }

        public async Task Handle([NotNull] UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _integrationEventService.PublishAsync(
                new UserClaimAddedIntegrationEvent(domainEvent.UserId, CustomClaimTypes.UserCustomerIdClaimType, domainEvent.CustomerId.ToString())
            );
        }
    }
}
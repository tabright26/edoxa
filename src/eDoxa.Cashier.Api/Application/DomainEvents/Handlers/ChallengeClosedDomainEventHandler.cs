// Filename: ChallengeClosedDomainEventHandler.cs
// Date Created: 2020-01-30
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Extensions;
using eDoxa.Cashier.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Cashier.Api.Application.DomainEvents.Handlers
{
    public sealed class ChallengeClosedDomainEventHandler : IDomainEventHandler<ChallengeClosedDomainEvent>
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger;

        public ChallengeClosedDomainEventHandler(IServiceBusPublisher serviceBusPublisher, ILogger<ChallengeClosedDomainEventHandler> logger)
        {
            _serviceBusPublisher = serviceBusPublisher;
            _logger = logger;
        }

        public async Task Handle(ChallengeClosedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _serviceBusPublisher.PublishChallengeClosedIntegrationEventAsync(domainEvent.ChallengeId, domainEvent.PayoutPrizes);

            _logger.LogInformation(""); // FRANCIS: TODO.
        }
    }
}

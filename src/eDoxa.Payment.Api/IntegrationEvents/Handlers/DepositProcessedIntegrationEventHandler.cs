// Filename: DepositProcessedIntegrationEventHandler.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.IntegrationEvents;
using eDoxa.Payment.Api.Providers.Stripe.Abstractions;

using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    internal sealed class DepositProcessedIntegrationEventHandler : IIntegrationEventHandler<DepositProcessedIntegrationEvent>
    {
        private readonly ILogger<DepositProcessedIntegrationEventHandler> _logger;
        private readonly IEventBusService _eventBusService;
        private readonly IStripeService _stripeService;

        public DepositProcessedIntegrationEventHandler(
            ILogger<DepositProcessedIntegrationEventHandler> logger,
            IEventBusService eventBusService,
            IStripeService stripeService
        )
        {
            _logger = logger;
            _eventBusService = eventBusService;
            _stripeService = stripeService;
        }

        public async Task Handle(DepositProcessedIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(DepositProcessedIntegrationEvent)}...");

                await _stripeService.CreateInvoiceAsync(
                    integrationEvent.TransactionId,
                    integrationEvent.TransactionDescription,
                    integrationEvent.CustomerId,
                    integrationEvent.Amount
                );

                _logger.LogInformation($"Processed {nameof(DepositProcessedIntegrationEvent)}.");

                _eventBusService.Publish(new TransactionSuccededIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(TransactionSuccededIntegrationEvent)}.");
            }
            catch (StripeException exception)
            {
                _logger.LogError(exception, exception.StripeError.ToJson());

                _eventBusService.Publish(new TransactionFailedIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(TransactionFailedIntegrationEvent)}.");
            }
        }
    }
}

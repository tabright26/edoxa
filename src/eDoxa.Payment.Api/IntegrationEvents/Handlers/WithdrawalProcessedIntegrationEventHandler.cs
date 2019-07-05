// Filename: WithdrawalProcessedIntegrationEventHandler.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.IntegrationEvents;
using eDoxa.Payment.Api.Providers.Stripe.Abstractions;

using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    internal sealed class WithdrawalProcessedIntegrationEventHandler : IIntegrationEventHandler<WithdrawalProcessedIntegrationEvent>
    {
        private readonly ILogger<WithdrawalProcessedIntegrationEventHandler> _logger;
        private readonly IEventBusService _eventBusService;
        private readonly IStripeService _stripeService;

        public WithdrawalProcessedIntegrationEventHandler(
            ILogger<WithdrawalProcessedIntegrationEventHandler> logger,
            IEventBusService eventBusService,
            IStripeService stripeService
        )
        {
            _logger = logger;
            _eventBusService = eventBusService;
            _stripeService = stripeService;
        }

        public async Task Handle(WithdrawalProcessedIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(WithdrawalProcessedIntegrationEvent)}...");

                await _stripeService.CreateTransferAsync(
                    integrationEvent.TransactionId,
                    integrationEvent.TransactionDescription,
                    integrationEvent.ConnectAccountId,
                    integrationEvent.Amount
                );

                _logger.LogInformation($"Processed {nameof(WithdrawalProcessedIntegrationEvent)}.");

                _eventBusService.Publish(new TransactionSuccededIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(TransactionSuccededIntegrationEvent)}.");
            }
            catch (StripeException exception)
            {
                _logger.LogError(exception, exception.StripeError.ToJson());

                _eventBusService.Publish(new TransactionFailedIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(TransactionFailedIntegrationEvent)}.");
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, $"Another exception type that {nameof(StripeException)} occurred.");

                _eventBusService.Publish(new TransactionFailedIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(TransactionFailedIntegrationEvent)}.");
            }
        }
    }
}

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

using eDoxa.Payment.Api.Providers.Stripe.Abstractions;
using eDoxa.Seedwork.IntegrationEvents;

using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    internal sealed class WithdrawalProcessedIntegrationEventHandler : IIntegrationEventHandler<WithdrawalProcessedIntegrationEvent>
    {
        private readonly ILogger<WithdrawalProcessedIntegrationEventHandler> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IStripeService _stripeService;

        public WithdrawalProcessedIntegrationEventHandler(
            ILogger<WithdrawalProcessedIntegrationEventHandler> logger,
            IServiceBusPublisher serviceBusPublisher,
            IStripeService stripeService
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _stripeService = stripeService;
        }

        public async Task HandleAsync(WithdrawalProcessedIntegrationEvent integrationEvent)
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

                _serviceBusPublisher.Publish(new TransactionSuccededIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(TransactionSuccededIntegrationEvent)}.");
            }
            catch (StripeException exception)
            {
                _logger.LogError(exception, exception.StripeError?.ToJson());

                _serviceBusPublisher.Publish(new TransactionFailedIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(TransactionFailedIntegrationEvent)}.");
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, $"Another exception type that {nameof(StripeException)} occurred.");

                _serviceBusPublisher.Publish(new TransactionFailedIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(TransactionFailedIntegrationEvent)}.");
            }
        }
    }
}

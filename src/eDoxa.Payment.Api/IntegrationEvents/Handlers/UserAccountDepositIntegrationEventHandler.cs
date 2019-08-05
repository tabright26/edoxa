﻿// Filename: DepositProcessedIntegrationEventHandler.cs
// Date Created: 2019-07-05
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
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    internal sealed class UserAccountDepositIntegrationEventHandler : IIntegrationEventHandler<UserAccountDepositIntegrationEvent>
    {
        private readonly ILogger<UserAccountDepositIntegrationEventHandler> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IStripeService _stripeService;

        public UserAccountDepositIntegrationEventHandler(
            ILogger<UserAccountDepositIntegrationEventHandler> logger,
            IServiceBusPublisher serviceBusPublisher,
            IStripeService stripeService
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _stripeService = stripeService;
        }

        public async Task HandleAsync(UserAccountDepositIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountDepositIntegrationEvent)}...");

                await _stripeService.CreateInvoiceAsync(
                    integrationEvent.TransactionId,
                    integrationEvent.TransactionDescription,
                    integrationEvent.CustomerId,
                    integrationEvent.Amount
                );

                _logger.LogInformation($"Processed {nameof(UserAccountDepositIntegrationEvent)}.");

                await _serviceBusPublisher.PublishAsync(new UserTransactionSuccededIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(UserTransactionSuccededIntegrationEvent)}.");
            }
            catch (StripeException exception)
            {
                _logger.LogError(exception, exception.StripeError?.ToJson());

                await _serviceBusPublisher.PublishAsync(new UserTransactionFailedIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, $"Another exception type that {nameof(StripeException)} occurred.");

                await _serviceBusPublisher.PublishAsync(new UserTransactionFailedIntegrationEvent(integrationEvent.TransactionId));

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }
        }
    }
}
// Filename: UserAccountWithdrawalIntegrationEventHandler.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Payment.Api.Areas.Stripe.Abstractions;
using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    internal sealed class UserAccountWithdrawalIntegrationEventHandler : IIntegrationEventHandler<UserAccountWithdrawalIntegrationEvent>
    {
        private readonly ILogger<UserAccountWithdrawalIntegrationEventHandler> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IStripeService _stripeService;

        public UserAccountWithdrawalIntegrationEventHandler(
            ILogger<UserAccountWithdrawalIntegrationEventHandler> logger,
            IServiceBusPublisher serviceBusPublisher,
            IStripeService stripeService
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _stripeService = stripeService;
        }

        public async Task HandleAsync(UserAccountWithdrawalIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountWithdrawalIntegrationEvent)}...");

                await _stripeService.CreateTransferAsync(
                    integrationEvent.TransactionId,
                    integrationEvent.TransactionDescription,
                    integrationEvent.ConnectAccountId,
                    integrationEvent.Amount);

                _logger.LogInformation($"Processed {nameof(UserAccountWithdrawalIntegrationEvent)}.");

                await _serviceBusPublisher.PublishUserTransactionSuccededIntegrationEventAsync(integrationEvent.TransactionId);

                _logger.LogInformation($"Published {nameof(UserTransactionSuccededIntegrationEvent)}.");
            }
            catch (StripeException exception)
            {
                _logger.LogError(exception, exception.StripeError?.ToJson());

                await _serviceBusPublisher.PublishUserTransactionFailedIntegrationEventAsync(integrationEvent.TransactionId);

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, $"Another exception type that {nameof(StripeException)} occurred.");

                await _serviceBusPublisher.PublishUserTransactionFailedIntegrationEventAsync(integrationEvent.TransactionId);

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }
        }
    }
}

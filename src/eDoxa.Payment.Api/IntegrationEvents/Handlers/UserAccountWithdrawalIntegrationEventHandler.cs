// Filename: UserAccountWithdrawalIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Payment.Domain.Services;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserAccountWithdrawalIntegrationEventHandler : IIntegrationEventHandler<UserAccountWithdrawalIntegrationEvent>
    {
        private readonly ILogger<UserAccountWithdrawalIntegrationEventHandler> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IStripeService _stripeService;
        private readonly IStripeConnectAccountService _stripeConnectAccountService;

        public UserAccountWithdrawalIntegrationEventHandler(
            ILogger<UserAccountWithdrawalIntegrationEventHandler> logger,
            IServiceBusPublisher serviceBusPublisher,
            IStripeService stripeService,
            IStripeConnectAccountService stripeConnectAccountService
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _stripeService = stripeService;
            _stripeConnectAccountService = stripeConnectAccountService;
        }

        public async Task HandleAsync(UserAccountWithdrawalIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountWithdrawalIntegrationEvent)}...");

                var connectAccountId = await _stripeConnectAccountService.GetConnectAccountIdAsync(integrationEvent.UserId);

                await _stripeService.CreateTransferAsync(
                    integrationEvent.TransactionId,
                    integrationEvent.Description,
                    connectAccountId,
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

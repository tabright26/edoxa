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
        private readonly IStripeTransferService _stripeTransferService;
        private readonly IStripeAccountService _stripeAccountService;

        public UserAccountWithdrawalIntegrationEventHandler(
            ILogger<UserAccountWithdrawalIntegrationEventHandler> logger,
            IServiceBusPublisher serviceBusPublisher,
            IStripeTransferService stripeTransferService,
            IStripeAccountService stripeAccountService
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _stripeTransferService = stripeTransferService;
            _stripeAccountService = stripeAccountService;
        }

        public async Task HandleAsync(UserAccountWithdrawalIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountWithdrawalIntegrationEvent)}...");

                var accountId = await _stripeAccountService.GetAccountIdAsync(integrationEvent.UserId);

                await _stripeTransferService.CreateTransferAsync(
                    integrationEvent.TransactionId,
                    integrationEvent.Description,
                    accountId,
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

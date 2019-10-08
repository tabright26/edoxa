// Filename: UserAccountDepositIntegrationEventHandler.cs
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
    public sealed class UserAccountDepositIntegrationEventHandler : IIntegrationEventHandler<UserAccountDepositIntegrationEvent>
    {
        private readonly ILogger<UserAccountDepositIntegrationEventHandler> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IStripeService _stripeService;
        private readonly IStripeCustomerService _stripeCustomerService;

        public UserAccountDepositIntegrationEventHandler(
            ILogger<UserAccountDepositIntegrationEventHandler> logger,
            IServiceBusPublisher serviceBusPublisher,
            IStripeService stripeService,
            IStripeCustomerService stripeCustomerService
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _stripeService = stripeService;
            _stripeCustomerService = stripeCustomerService;
        }

        public async Task HandleAsync(UserAccountDepositIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountDepositIntegrationEvent)}...");

                var customerId = await _stripeCustomerService.GetCustomerIdAsync(integrationEvent.UserId);

                await _stripeService.CreateInvoiceAsync(
                    integrationEvent.TransactionId,
                    integrationEvent.Description,
                    customerId,
                    integrationEvent.Amount);

                _logger.LogInformation($"Processed {nameof(UserAccountDepositIntegrationEvent)}.");

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

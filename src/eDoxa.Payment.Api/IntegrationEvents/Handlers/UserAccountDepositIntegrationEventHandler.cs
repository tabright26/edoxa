// Filename: UserAccountDepositIntegrationEventHandler.cs
// Date Created: 2019-10-06
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    public sealed class UserAccountDepositIntegrationEventHandler : IIntegrationEventHandler<UserAccountDepositIntegrationEvent>
    {
        private readonly ILogger<UserAccountDepositIntegrationEventHandler> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IStripeInvoiceService _stripeInvoiceService;
        private readonly IStripeCustomerService _stripeCustomerService;

        public UserAccountDepositIntegrationEventHandler(
            ILogger<UserAccountDepositIntegrationEventHandler> logger,
            IServiceBusPublisher serviceBusPublisher,
            IStripeInvoiceService stripeInvoiceService,
            IStripeCustomerService stripeCustomerService
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _stripeInvoiceService = stripeInvoiceService;
            _stripeCustomerService = stripeCustomerService;
        }

        public async Task HandleAsync(UserAccountDepositIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountDepositIntegrationEvent)}...");

                var customerId = await _stripeCustomerService.GetCustomerIdAsync(integrationEvent.UserId);

                if (!await _stripeCustomerService.HasDefaultPaymentMethodAsync(customerId))
                {
                    throw new InvalidOperationException(
                        "The user's Stripe Customer has no default payment method. The user's cannot process a deposit transaction.");
                }

                await _stripeInvoiceService.CreateInvoiceAsync(
                    customerId,
                    integrationEvent.TransactionId,
                    integrationEvent.Amount,
                    integrationEvent.Description);

                _logger.LogInformation($"Processed {nameof(UserAccountDepositIntegrationEvent)}.");

                await _serviceBusPublisher.PublishUserTransactionSuccededIntegrationEventAsync(integrationEvent.UserId, integrationEvent.TransactionId);

                _logger.LogInformation($"Published {nameof(UserTransactionSuccededIntegrationEvent)}.");
            }
            catch (Exception exception)
            {
                if (exception is StripeException stripeException)
                {
                    _logger.LogCritical(stripeException, stripeException.StripeError?.ToJson());
                }
                else
                {
                    _logger.LogCritical(exception, $"Another exception type that {nameof(StripeException)} occurred.");
                }

                await _serviceBusPublisher.PublishUserTransactionFailedIntegrationEventAsync(integrationEvent.UserId, integrationEvent.TransactionId);

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }
        }
    }
}

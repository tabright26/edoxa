// Filename: UserAccountDepositIntegrationEventHandler.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Misc;
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
            var userId = UserId.Parse(integrationEvent.UserId);

            var transactionId = TransactionId.Parse(integrationEvent.TransactionId);

            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountDepositIntegrationEvent)}...");

                var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

                if (!await _stripeCustomerService.HasDefaultPaymentMethodAsync(customerId))
                {
                    throw new InvalidOperationException(
                        "The user's Stripe Customer has no default payment method. The user's cannot process a deposit transaction.");
                }

                await _stripeInvoiceService.CreateInvoiceAsync(
                    customerId,
                    transactionId,
                    integrationEvent.Amount,
                    integrationEvent.Description);

                _logger.LogInformation($"Processed {nameof(UserAccountDepositIntegrationEvent)}.");

                await _serviceBusPublisher.PublishUserTransactionSuccededIntegrationEventAsync(userId, transactionId);

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

                await _serviceBusPublisher.PublishUserTransactionFailedIntegrationEventAsync(userId, transactionId);

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }
        }
    }
}

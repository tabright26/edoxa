// Filename: UserAccountWithdrawalIntegrationEventHandler.cs
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

                // GABRIEL: UNIT TEST NEED TO BE UPDATED.
                if (!await _stripeAccountService.HasAccountVerifiedAsync(accountId))
                {
                    throw new InvalidOperationException("The user's Stripe Account isn't verified. The user's cannot process a withdrawal transaction.");
                }

                await _stripeTransferService.CreateTransferAsync(
                    accountId,
                    integrationEvent.TransactionId,
                    integrationEvent.Amount,
                    integrationEvent.Description);

                _logger.LogInformation($"Processed {nameof(UserAccountWithdrawalIntegrationEvent)}.");

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

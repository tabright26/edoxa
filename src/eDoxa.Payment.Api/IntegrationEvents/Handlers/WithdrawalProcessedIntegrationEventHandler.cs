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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.IntegrationEvents;
using eDoxa.Payment.Api.Providers.Stripe;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Payment.Api.IntegrationEvents.Handlers
{
    internal sealed class WithdrawalProcessedIntegrationEventHandler : IIntegrationEventHandler<WithdrawalProcessedIntegrationEvent>
    {
        private readonly ILogger<WithdrawalProcessedIntegrationEventHandler> _logger;
        private readonly IEventBusService _eventBusService;
        private readonly TransferService _transferService;
        private readonly StripeOptions _stripeOptions;

        public WithdrawalProcessedIntegrationEventHandler(
            ILogger<WithdrawalProcessedIntegrationEventHandler> logger,
            IEventBusService eventBusService,
            IOptionsSnapshot<StripeOptions> options,
            TransferService transferService
        )
        {
            _logger = logger;
            _eventBusService = eventBusService;
            _stripeOptions = options.Value;
            _transferService = transferService;
        }

        public async Task Handle(WithdrawalProcessedIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(WithdrawalProcessedIntegrationEvent)}...");

                await this.StripeWithdrawalAsync(
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
        }

        private async Task StripeWithdrawalAsync(
            Guid transactionId,
            string transactionDescription,
            string connectAccountId,
            long amount,
            CancellationToken cancellationToken = default
        )
        {
            var options = new TransferCreateOptions
            {
                Destination = connectAccountId,
                Currency = _stripeOptions.Currency,
                Amount = amount,
                Description = transactionDescription,
                Metadata = new Dictionary<string, string>
                {
                    ["TransactionId"] = transactionId.ToString()
                }
            };

            await _transferService.CreateAsync(options, cancellationToken: cancellationToken);
        }
    }
}

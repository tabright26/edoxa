// Filename: DepositProcessedIntegrationEventHandler.cs
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
    internal sealed class DepositProcessedIntegrationEventHandler : IIntegrationEventHandler<DepositProcessedIntegrationEvent>
    {
        private readonly ILogger<DepositProcessedIntegrationEventHandler> _logger;
        private readonly IEventBusService _eventBusService;
        private readonly StripeOptions _stripeOptions;
        private readonly InvoiceService _invoiceService;
        private readonly InvoiceItemService _invoiceItemService;

        public DepositProcessedIntegrationEventHandler(
            ILogger<DepositProcessedIntegrationEventHandler> logger,
            IEventBusService eventBusService,
            IOptionsSnapshot<StripeOptions> options,
            InvoiceService invoiceService,
            InvoiceItemService invoiceItemService
        )
        {
            _logger = logger;
            _eventBusService = eventBusService;
            _stripeOptions = options.Value;
            _invoiceService = invoiceService;
            _invoiceItemService = invoiceItemService;
        }

        public async Task Handle(DepositProcessedIntegrationEvent integrationEvent)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(DepositProcessedIntegrationEvent)}...");

                await this.StripeDepositAsync(
                    integrationEvent.TransactionId,
                    integrationEvent.TransactionDescription,
                    integrationEvent.CustomerId,
                    integrationEvent.Amount
                );

                _logger.LogInformation($"Processed {nameof(DepositProcessedIntegrationEvent)}.");

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

        private async Task StripeDepositAsync(
            Guid transactionId,
            string transactionDescription,
            string customerId,
            long amount,
            CancellationToken cancellationToken = default
        )
        {
            await this.CreateInvoiceItemAsync(
                transactionId,
                transactionDescription,
                customerId,
                amount,
                cancellationToken
            );

            await this.CreateInvoiceAsync(transactionId, customerId, cancellationToken);
        }

        private async Task CreateInvoiceItemAsync(
            Guid transactionId,
            string transactionDescription,
            string customerId,
            long amount,
            CancellationToken cancellationToken = default
        )
        {
            var options = new InvoiceItemCreateOptions
            {
                CustomerId = customerId,
                Currency = _stripeOptions.Currency,
                Amount = amount,
                Description = transactionDescription,
                TaxRates = _stripeOptions.TaxRateIds,
                Metadata = new Dictionary<string, string>
                {
                    ["TransactionId"] = transactionId.ToString()
                }
            };

            await _invoiceItemService.CreateAsync(options, cancellationToken: cancellationToken);
        }

        private async Task CreateInvoiceAsync(Guid transactionId, string customerId, CancellationToken cancellationToken = default)
        {
            var options = new InvoiceCreateOptions
            {
                CustomerId = customerId,
                AutoAdvance = true,
                Metadata = new Dictionary<string, string>
                {
                    ["TransactionId"] = transactionId.ToString()
                }
            };

            await _invoiceService.CreateAsync(options, cancellationToken: cancellationToken);
        }
    }
}

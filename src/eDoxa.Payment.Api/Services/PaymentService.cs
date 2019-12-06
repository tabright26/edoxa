// Filename: PaymentService.cs
// Date Created: 2019-12-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos;
using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Grpc.Core;

using Microsoft.Extensions.Logging;

using Stripe;

using static eDoxa.Grpc.Protos.PaymentService;

namespace eDoxa.Payment.Api.Services
{
    public sealed class PaymentService : PaymentServiceBase
    {
        private readonly ILogger<UserAccountWithdrawalIntegrationEventHandler> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IStripeTransferService _stripeTransferService;
        private readonly IStripeAccountService _stripeAccountService;
        private readonly IStripeInvoiceService _stripeInvoiceService;
        private readonly IStripeCustomerService _stripeCustomerService;

        public PaymentService(
            ILogger<UserAccountWithdrawalIntegrationEventHandler> logger,
            IServiceBusPublisher serviceBusPublisher,
            IStripeTransferService stripeTransferService,
            IStripeAccountService stripeAccountService,
            IStripeInvoiceService stripeInvoiceService,
            IStripeCustomerService stripeCustomerService
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _stripeTransferService = stripeTransferService;
            _stripeAccountService = stripeAccountService;
            _stripeInvoiceService = stripeInvoiceService;
            _stripeCustomerService = stripeCustomerService;
        }

        public override async Task<DepositResponse> Deposit(DepositRequest request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountDepositIntegrationEvent)}...");

                var customerId = await _stripeCustomerService.GetCustomerIdAsync(UserId.Parse(request.UserId));

                if (!await _stripeCustomerService.HasDefaultPaymentMethodAsync(customerId))
                {
                    throw new InvalidOperationException(
                        "The user's Stripe Customer has no default payment method. The user's cannot process a deposit transaction.");
                }

                await _stripeInvoiceService.CreateInvoiceAsync(
                    customerId,
                    TransactionId.Parse(request.TransactionId),
                    request.Amount,
                    request.Description);

                _logger.LogInformation($"Processed {nameof(UserAccountDepositIntegrationEvent)}.");

                await _serviceBusPublisher.PublishUserTransactionSuccededIntegrationEventAsync(
                    UserId.Parse(request.UserId),
                    TransactionId.Parse(request.TransactionId));

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

                await _serviceBusPublisher.PublishUserTransactionFailedIntegrationEventAsync(
                    UserId.Parse(request.UserId),
                    TransactionId.Parse(request.TransactionId));

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }

            return new DepositResponse
            {
                StatusCode = 200
            };
        }

        public override async Task<WithdrawalResponse> Withdrawal(WithdrawalRequest request, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountWithdrawalIntegrationEvent)}...");

                var accountId = await _stripeAccountService.GetAccountIdAsync(UserId.Parse(request.UserId));

                if (!await _stripeAccountService.HasAccountVerifiedAsync(accountId))
                {
                    throw new InvalidOperationException("The user's Stripe Account isn't verified. The user's cannot process a withdrawal transaction.");
                }

                await _stripeTransferService.CreateTransferAsync(
                    accountId,
                    TransactionId.Parse(request.TransactionId),
                    request.Amount,
                    request.Description);

                _logger.LogInformation($"Processed {nameof(UserAccountWithdrawalIntegrationEvent)}.");

                await _serviceBusPublisher.PublishUserTransactionSuccededIntegrationEventAsync(
                    UserId.Parse(request.UserId),
                    TransactionId.Parse(request.TransactionId));

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

                await _serviceBusPublisher.PublishUserTransactionFailedIntegrationEventAsync(
                    UserId.Parse(request.UserId),
                    TransactionId.Parse(request.TransactionId));

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }

            return new WithdrawalResponse
            {
                StatusCode = 200
            };
        }
    }
}

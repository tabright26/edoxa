// Filename: PaymentService.cs
// Date Created: 2019-12-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.Grpc.Protos;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Grpc.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.Services
{
    [Authorize]
    public sealed class PaymentGrpcService : PaymentService.PaymentServiceBase
    {
        private readonly ILogger<UserAccountWithdrawalIntegrationEventHandler> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IStripeTransferService _stripeTransferService;
        private readonly IStripeAccountService _stripeAccountService;
        private readonly IStripeInvoiceService _stripeInvoiceService;
        private readonly IStripeCustomerService _stripeCustomerService;

        public PaymentGrpcService(
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
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

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
                    TransactionId.Parse(request.TransactionId),
                    request.Amount,
                    request.Description);

                _logger.LogInformation($"Processed {nameof(UserAccountDepositIntegrationEvent)}.");

                await _serviceBusPublisher.PublishUserTransactionSuccededIntegrationEventAsync(
                    userId,
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
                    userId,
                    TransactionId.Parse(request.TransactionId));

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }

            return new DepositResponse();
        }

        public override async Task<WithdrawalResponse> Withdrawal(WithdrawalRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            try
            {
                _logger.LogInformation($"Processing {nameof(UserAccountWithdrawalIntegrationEvent)}...");

                var accountId = await _stripeAccountService.GetAccountIdAsync(userId);

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
                    userId,
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
                    userId,
                    TransactionId.Parse(request.TransactionId));

                _logger.LogInformation($"Published {nameof(UserTransactionFailedIntegrationEvent)}.");
            }

            return new WithdrawalResponse();
        }
    }
}

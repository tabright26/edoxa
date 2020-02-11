﻿// Filename: PaymentGrpcService.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Grpc.Protos.Payment.Responses;
using eDoxa.Grpc.Protos.Payment.Services;
using eDoxa.Payment.Api.Extensions;
using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Paypal.Services.Abstractions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;
using eDoxa.Stripe.Services.Abstractions;

using Grpc.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

using Stripe;

namespace eDoxa.Payment.Api.Grpc.Services
{
    [Authorize]
    public sealed class PaymentGrpcService : PaymentService.PaymentServiceBase
    {
        private readonly ILogger<PaymentGrpcService> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IStripeInvoiceService _stripeInvoiceService;
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IPaypalPayoutService _paypalPayoutService;

        public PaymentGrpcService(
            ILogger<PaymentGrpcService> logger,
            IServiceBusPublisher serviceBusPublisher,
            IStripeInvoiceService stripeInvoiceService,
            IStripeCustomerService stripeCustomerService,
            IPaypalPayoutService paypalPayoutService
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _stripeInvoiceService = stripeInvoiceService;
            _stripeCustomerService = stripeCustomerService;
            _paypalPayoutService = paypalPayoutService;
        }

        public override async Task<DepositResponse> Deposit(DepositRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var email = httpContext.GetEmail();

            var customerId = httpContext.GetStripeCustomertId();

            try
            {
                if (!await _stripeCustomerService.HasDefaultPaymentMethodAsync(customerId))
                {
                    const string detail = "The user's Stripe Customer has no default payment method. The user's cannot process a deposit transaction.";

                    throw context.RpcException(new Status(StatusCode.FailedPrecondition, detail));
                }

                var invoice = await _stripeInvoiceService.CreateInvoiceAsync(
                    customerId,
                    request.Transaction.Id.ParseEntityId<TransactionId>(),
                    request.Transaction.Currency.ToCents(),
                    request.Transaction.Description);

                await _serviceBusPublisher.PublishUserDepositSucceededIntegrationEventAsync(userId, request.Transaction);

                var response = new DepositResponse();

                var message = $"A Stripe invoice '{invoice.Id}' was created for the user '{email}'. (userId=\"{userId}\")";

                return context.Ok(response, message);
            }
            catch (Exception exception)
            {
                await _serviceBusPublisher.PublishUserDepositFailedIntegrationEventAsync(userId, request.Transaction);

                var message = $"Failed to process deposit for the user '{email}'. (userId=\"{userId}\")";

                throw this.RpcExceptionWithInternalStatus(exception, message);
            }
        }

        public override async Task<WithdrawalResponse> Withdrawal(WithdrawalRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var email = httpContext.GetEmail();

            try
            {
                var payoutBatch = await _paypalPayoutService.CreateAsync(
                    request.Transaction.Id.ParseEntityId<TransactionId>(),
                    request.Email,
                    -request.Transaction.Currency.Amount.ToDecimal(),
                    request.Transaction.Description);

                await _serviceBusPublisher.PublishUserWithdrawalSucceededIntegrationEventAsync(userId, request.Transaction);

                var response = new WithdrawalResponse();

                var message = $"A PayPal payout batch '{payoutBatch.batch_header.payout_batch_id}' was created for the user '{email}'. (userId=\"{userId}\")";

                return context.Ok(response, message);
            }
            catch (Exception exception)
            {
                await _serviceBusPublisher.PublishUserWithdrawalFailedIntegrationEventAsync(userId, request.Transaction);

                var message = $"Failed to process withdrawal for the user '{email}'. (userId=\"{userId}\")";

                throw this.RpcExceptionWithInternalStatus(exception, message);
            }
        }

        private RpcException RpcExceptionWithInternalStatus(Exception exception, string message)
        {
            _logger.LogError(exception, message);

            if (exception is StripeException stripeException)
            {
                return new RpcException(
                    new Status(StatusCode.Internal, message),
                    new Metadata
                    {
                        {nameof(StripeException), stripeException.StripeError?.ToJson()}
                    });
            }

            return new RpcException(new Status(StatusCode.Internal, message));
        }
    }
}

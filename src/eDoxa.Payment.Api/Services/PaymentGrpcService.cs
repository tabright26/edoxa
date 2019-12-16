﻿// Filename: PaymentGrpcService.cs
// Date Created: 2019-12-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Grpc.Protos.Payment.Responses;
using eDoxa.Grpc.Protos.Payment.Services;
using eDoxa.Payment.Api.Application.Stripe.Extensions;
using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
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

            var customerId = httpContext.GetStripeCustomertId();

            var email = httpContext.GetEmail();

            try
            {
                if (!await _stripeCustomerService.HasDefaultPaymentMethodAsync(customerId))
                {
                    const string detail = "The user's Stripe Customer has no default payment method. The user's cannot process a deposit transaction.";

                    throw context.RpcException(new Status(StatusCode.FailedPrecondition, detail));
                }

                var invoice = await _stripeInvoiceService.CreateInvoiceAsync(
                    customerId,
                    request.TransactionId.ParseEntityId<TransactionId>(),
                    request.Amount,
                    request.Description);

                await _serviceBusPublisher.PublishUserTransactionSuccededIntegrationEventAsync(userId, TransactionId.Parse(request.TransactionId));

                var response = new DepositResponse();

                var message = $"A Stripe invoice '{invoice.Id}' was created for the user '{email}'. (userId=\"{userId}\")";

                return context.Ok(response, message);
            }
            catch (Exception exception)
            {
                await _serviceBusPublisher.PublishUserTransactionFailedIntegrationEventAsync(userId, TransactionId.Parse(request.TransactionId));

                var message = $"Failed to process deposit for the user '{email}'. (userId=\"{userId}\")";

                throw this.RpcExceptionWithInternalStatus(exception, message); // TODO
            }
        }

        public override async Task<WithdrawalResponse> Withdrawal(WithdrawalRequest request, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();

            var userId = httpContext.GetUserId();

            var email = httpContext.GetEmail();

            var accountId = httpContext.GetStripeAccountId();

            try
            {
                if (!await _stripeAccountService.HasAccountVerifiedAsync(accountId))
                {
                    const string detail = "The user's Stripe Account isn't verified. The user's cannot process a withdrawal transaction.";

                    throw context.RpcException(new Status(StatusCode.FailedPrecondition, detail));
                }

                var transfer = await _stripeTransferService.CreateTransferAsync(
                    accountId,
                    request.TransactionId.ParseEntityId<TransactionId>(),
                    request.Amount,
                    request.Description);

                await _serviceBusPublisher.PublishUserTransactionSuccededIntegrationEventAsync(userId, TransactionId.Parse(request.TransactionId));

                var response = new WithdrawalResponse();

                var message = $"A Stripe transfer '{transfer.Id}' was created for the user '{email}'. (userId=\"{userId}\")";

                return context.Ok(response, message);
            }
            catch (Exception exception)
            {
                await _serviceBusPublisher.PublishUserTransactionFailedIntegrationEventAsync(userId, TransactionId.Parse(request.TransactionId));

                var message = $"Failed to process withdrawal for the user '{email}'. (userId=\"{userId}\")";

                throw this.RpcExceptionWithInternalStatus(exception, message); // TODO
            }
        }

        private RpcException RpcExceptionWithInternalStatus(Exception exception, string message) // TODO
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

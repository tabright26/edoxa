// Filename: PaymentGrpcService.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
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
using eDoxa.Stripe;

using Grpc.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Stripe;

namespace eDoxa.Payment.Api.Grpc.Services
{
    [Authorize]
    public sealed class PaymentGrpcService : PaymentService.PaymentServiceBase
    {
        private readonly ILogger<PaymentGrpcService> _logger;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly IPaypalPayoutService _paypalPayoutService;
        private readonly IOptionsSnapshot<StripeOptions> _stripeOptions;

        public PaymentGrpcService(
            ILogger<PaymentGrpcService> logger,
            IServiceBusPublisher serviceBusPublisher,
            IPaypalPayoutService paypalPayoutService,
            IOptionsSnapshot<StripeOptions> stripeOptions
        )
        {
            _logger = logger;
            _serviceBusPublisher = serviceBusPublisher;
            _paypalPayoutService = paypalPayoutService;
            _stripeOptions = stripeOptions;
        }

        private StripeOptions Options => _stripeOptions.Value;

        public override async Task<CreateStripePaymentIntentResponse> CreateStripePaymentIntent(
            CreateStripePaymentIntentRequest request,
            ServerCallContext context
        )
        {
            //try
            //{
            var httpContext = context.GetHttpContext();

            var paymentIntentService = new PaymentIntentService();

            var options = new PaymentIntentCreateOptions
            {
                PaymentMethod = request.PaymentMethodId,
                Customer = httpContext.GetStripeCustomerId(),
                ReceiptEmail = httpContext.GetEmail(),
                Amount = request.Transaction.Currency.ToCents(),
                Currency = Options.Invoice.Currency,
                Metadata = new Dictionary<string, string>
                {
                    ["UserId"] = httpContext.GetUserId(),
                    ["TransactionId"] = request.Transaction.Id
                }
            };

            var paymentIntent = await paymentIntentService.CreateAsync(options);

            var response = new CreateStripePaymentIntentResponse
            {
                ClientSecret = paymentIntent.ClientSecret
            };

            var message = $"A new payment {paymentIntent.Id} for {paymentIntent.Amount} {paymentIntent.Currency} was created";

            return context.Ok(response, message);

            //if (!await _stripeCustomerService.HasDefaultPaymentMethodAsync(customerId))
            //{
            //    const string detail = "The user's Stripe Customer has no default payment method. The user's cannot process a deposit transaction.";

            //    throw context.RpcException(new Status(StatusCode.FailedPrecondition, detail));
            //}

            //var invoice = await _stripeInvoiceService.CreateInvoiceAsync(
            //    customerId,
            //    request.Transaction.Id.ParseEntityId<TransactionId>(),
            //    request.Transaction.Currency.ToCents(),
            //    request.Transaction.Description);

            //await _serviceBusPublisher.PublishUserDepositSucceededIntegrationEventAsync(userId, request.Transaction);

            //var response = new DepositResponse();

            //var message = $"A Stripe invoice '{invoice.Id}' was created for the user '{email}'. (userId=\"{userId}\")";

            //return context.Ok(response, message);
            //}
            //catch (Exception exception)
            //{
            //    await _serviceBusPublisher.PublishUserDepositFailedIntegrationEventAsync(httpContext.GetUserId(), request.Transaction);

            //    var message = $"Failed to process deposit for the user '{httpContext.GetEmail()}'. (userId=\"{httpContext.GetUserId()}\")";

            //    throw this.RpcExceptionWithInternalStatus(exception, message);
            //}
        }

        public override async Task<CreatePaypalPayoutResponse> CreatePaypalPayout(CreatePaypalPayoutRequest request, ServerCallContext context)
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

                var response = new CreatePaypalPayoutResponse();

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

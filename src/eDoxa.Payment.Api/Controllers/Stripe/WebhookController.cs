// Filename: WebhookController.cs
// Date Created: 2020-02-13
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.IO;
using System.Threading.Tasks;

using eDoxa.Payment.Api.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Stripe;

namespace eDoxa.Payment.Api.Controllers.Stripe
{
    [Route("api/stripe/webhook")]
    public class StripeWebHook : Controller
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger;

        public StripeWebHook(IServiceBusPublisher serviceBusPublisher, ILogger<StripeWebHook> logger)
        {
            _serviceBusPublisher = serviceBusPublisher;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentCreated:
                    {
                        var paymentIntent = (PaymentIntent) stripeEvent.Data.Object;

                        _logger.LogDebug(Events.PaymentIntentCreated);

                        _logger.LogDebug(JsonConvert.SerializeObject(paymentIntent, Formatting.Indented));

                        break;
                    }

                    case Events.PaymentIntentCanceled:
                    {
                        var paymentIntent = (PaymentIntent) stripeEvent.Data.Object;

                        _logger.LogDebug(Events.PaymentIntentCanceled);

                        _logger.LogDebug(JsonConvert.SerializeObject(paymentIntent, Formatting.Indented));

                        await _serviceBusPublisher.PublishUserStripePaymentIntentCanceledIntegrationEventAsync(
                            UserId.Parse(paymentIntent!.Metadata["UserId"]),
                            TransactionId.Parse(paymentIntent.Metadata["TransactionId"]));

                        break;
                    }

                    case Events.PaymentIntentSucceeded:
                    {
                        var paymentIntent = (PaymentIntent) stripeEvent.Data.Object;

                        _logger.LogDebug(Events.PaymentIntentSucceeded);

                        _logger.LogDebug(JsonConvert.SerializeObject(paymentIntent, Formatting.Indented));

                        await _serviceBusPublisher.PublishUserStripePaymentIntentSucceededIntegrationEventAsync(
                            UserId.Parse(paymentIntent!.Metadata["UserId"]),
                            TransactionId.Parse(paymentIntent.Metadata["TransactionId"]));

                        break;
                    }

                    case Events.PaymentIntentPaymentFailed:
                    {
                        var paymentIntent = (PaymentIntent) stripeEvent.Data.Object;

                        _logger.LogDebug(Events.PaymentIntentPaymentFailed);

                        _logger.LogDebug(JsonConvert.SerializeObject(paymentIntent, Formatting.Indented));

                        await _serviceBusPublisher.PublishUserStripePaymentIntentPaymentFailedIntegrationEventAsync(
                            UserId.Parse(paymentIntent!.Metadata["UserId"]),
                            TransactionId.Parse(paymentIntent.Metadata["TransactionId"]));

                        break;
                    }

                    case Events.PaymentMethodAttached:
                    {
                        var paymentMethod = (PaymentMethod) stripeEvent.Data.Object;

                        _logger.LogDebug(Events.PaymentMethodAttached);

                        _logger.LogDebug(JsonConvert.SerializeObject(paymentMethod, Formatting.Indented));

                        break;
                    }
                    default:
                    {
                        return this.BadRequest();
                    }
                }

                return this.Ok();
            }
            catch (StripeException exception)
            {
                return this.BadRequest();
            }
        }
    }
}

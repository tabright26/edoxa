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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Stripe;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Payment.Api.Controllers.Stripe
{
    [Route("api/stripe/webhook")]
    public class WebhookController : Controller
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger;

        public WebhookController(IServiceBusPublisher serviceBusPublisher, ILogger<WebhookController> logger)
        {
            _serviceBusPublisher = serviceBusPublisher;
            _logger = logger;
        }

        [HttpPost]
        [SwaggerOperation("Handle Stripe webhook.")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> HandleWebhookAsync()
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
                _logger.LogError(exception, "Stripe webhook error.");

                return this.BadRequest();
            }
        }
    }
}

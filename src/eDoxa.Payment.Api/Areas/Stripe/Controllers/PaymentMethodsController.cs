// Filename: PaymentMethodsController.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.Areas.Stripe.Requests;
using eDoxa.Payment.Api.Extensions;
using eDoxa.Payment.Domain.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/stripe/payment-methods")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class PaymentMethodsController : ControllerBase
    {
        private readonly IStripePaymentMethodService _stripePaymentMethodService;
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeService _stripeService;

        public PaymentMethodsController(
            IStripePaymentMethodService stripePaymentMethodService,
            IStripeCustomerService stripeCustomerService,
            IStripeService stripeService
        )
        {
            _stripePaymentMethodService = stripePaymentMethodService;
            _stripeCustomerService = stripeCustomerService;
            _stripeService = stripeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string type)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                if (!await _stripeService.ReferenceExistsAsync(userId))
                {
                    return this.NotFound("Stripe reference not found.");
                }

                var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

                var paymentMethods = await _stripePaymentMethodService.FetchPaymentMethodsAsync(customerId, type);

                return this.Ok(paymentMethods);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }

        [HttpPut("{paymentMethodId}")]
        public async Task<IActionResult> PostAsync(string paymentMethodId, [FromBody] PaymentMethodPutRequest request)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                if (!await _stripeService.ReferenceExistsAsync(userId))
                {
                    return this.NotFound("Stripe reference not found.");
                }

                var paymentMethod = await _stripePaymentMethodService.UpdatePaymentMethodAsync(paymentMethodId, request.ExpMonth, request.ExpYear);

                return this.Ok(paymentMethod);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

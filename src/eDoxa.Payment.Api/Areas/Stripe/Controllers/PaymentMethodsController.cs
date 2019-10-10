// Filename: PaymentMethodsController.cs
// Date Created: 2019-10-08
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
        private readonly PaymentMethodService _paymentMethodService;
        private readonly IStripeCustomerService _stripeCustomerService;

        public PaymentMethodsController(PaymentMethodService paymentMethodService, IStripeCustomerService stripeCustomerService)
        {
            _paymentMethodService = paymentMethodService;
            _stripeCustomerService = stripeCustomerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] string type)
        {
            var userId = HttpContext.GetUserId();

            var customerId = await _stripeCustomerService.FindCustomerIdAsync(userId);

            if (customerId == null)
            {
                return this.NotFound("User not found.");
            }

            try
            {
                var paymentMethods = await _paymentMethodService.ListAsync(
                    new PaymentMethodListOptions
                    {
                        CustomerId = customerId,
                        Type = type
                    });

                return this.Ok(paymentMethods);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }

        [HttpPost("{paymentMethodId}")]
        public async Task<IActionResult> PostAsync(string paymentMethodId, [FromBody] PaymentMethodPostRequest request)
        {
            var userId = HttpContext.GetUserId();

            var customerId = await _stripeCustomerService.FindCustomerIdAsync(userId);

            if (customerId == null)
            {
                return this.NotFound("User not found.");
            }

            try
            {
                var paymentMethod = await _paymentMethodService.UpdateAsync(
                    paymentMethodId,
                    new PaymentMethodUpdateOptions
                    {
                        Card = new PaymentMethodCardUpdateOptions
                        {
                            ExpMonth = request.ExpMonth,
                            ExpYear = request.ExpYear
                        }
                    });

                return this.Ok(paymentMethod);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

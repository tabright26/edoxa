// Filename: PaymentMethodAttachController.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

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
    [Route("api/stripe/payment-methods/{paymentMethodId}/attach")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class PaymentMethodAttachController : ControllerBase
    {
        private readonly IStripePaymentMethodService _stripePaymentMethodService;
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeService _stripeService;

        public PaymentMethodAttachController(
            IStripePaymentMethodService stripePaymentMethodService,
            IStripeCustomerService stripeCustomerService,
            IStripeService stripeService
        )
        {
            _stripePaymentMethodService = stripePaymentMethodService;
            _stripeCustomerService = stripeCustomerService;
            _stripeService = stripeService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(string paymentMethodId)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                if (!await _stripeService.ReferenceExistsAsync(userId))
                {
                    return this.NotFound("Stripe reference not found.");
                }

                var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

                var paymentMethod = await _stripePaymentMethodService.AttachPaymentMethodAsync(paymentMethodId, customerId);

                return this.Ok(paymentMethod);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

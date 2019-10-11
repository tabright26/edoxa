// Filename: PaymentMethodDetachController.cs
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
    [Route("api/stripe/payment-methods/{paymentMethodId}/detach")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class PaymentMethodDetachController : ControllerBase
    {
        private readonly IStripePaymentMethodService _stripePaymentMethodService;
        private readonly IStripeCustomerService _stripeCustomerService;

        public PaymentMethodDetachController(IStripePaymentMethodService stripePaymentMethodService, IStripeCustomerService stripeCustomerService)
        {
            _stripePaymentMethodService = stripePaymentMethodService;
            _stripeCustomerService = stripeCustomerService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(string paymentMethodId)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                var customerId = await _stripeCustomerService.FindCustomerIdAsync(userId);

                if (customerId == null)
                {
                    return this.NotFound("User not found.");
                }

                var paymentMethod = await _stripePaymentMethodService.DetachPaymentMethodAsync(paymentMethodId);

                return this.Ok(paymentMethod);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

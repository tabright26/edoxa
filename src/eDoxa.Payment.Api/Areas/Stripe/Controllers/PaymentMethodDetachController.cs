// Filename: PaymentMethodDetachController.cs
// Date Created: 2019-10-08
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
        private readonly PaymentMethodService _paymentMethodService;
        private readonly IStripeCustomerService _stripeCustomerService;

        public PaymentMethodDetachController(PaymentMethodService paymentMethodService, IStripeCustomerService stripeCustomerService)
        {
            _paymentMethodService = paymentMethodService;
            _stripeCustomerService = stripeCustomerService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(string paymentMethodId)
        {
            var userId = HttpContext.GetUserId();

            var customerId = await _stripeCustomerService.FindCustomerIdAsync(userId);

            if (customerId == null)
            {
                return this.NotFound("User not found.");
            }

            try
            {
                var paymentMethod = await _paymentMethodService.DetachAsync(paymentMethodId, new PaymentMethodDetachOptions());

                return this.Ok(paymentMethod);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

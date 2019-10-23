// Filename: CustomerPaymentMethodDefaultController.cs
// Date Created: 2019-10-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.Extensions;
using eDoxa.Payment.Domain.Stripe.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/stripe/customer/payment-methods/{paymentMethodId}/default")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class CustomerPaymentMethodDefaultController : ControllerBase
    {
        private readonly IStripeReferenceService _stripeReferenceService;
        private readonly IStripeCustomerService _stripeCustomerService;

        public CustomerPaymentMethodDefaultController(IStripeReferenceService stripeReferenceService, IStripeCustomerService stripeCustomerService)
        {
            _stripeReferenceService = stripeReferenceService;
            _stripeCustomerService = stripeCustomerService;
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(string paymentMethodId)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                if (!await _stripeReferenceService.ReferenceExistsAsync(userId))
                {
                    return this.NotFound("Stripe reference not found.");
                }

                var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

                var customer = await _stripeCustomerService.SetDefaultPaymentMethodAsync(customerId, paymentMethodId);

                return this.Ok(customer);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

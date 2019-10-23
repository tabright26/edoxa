// Filename: CustomerController.cs
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
    [Route("api/stripe/customer")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class CustomerController : ControllerBase
    {
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeReferenceService _stripeReferenceService;

        public CustomerController(IStripeCustomerService stripeCustomerService, IStripeReferenceService stripeReferenceService)
        {
            _stripeCustomerService = stripeCustomerService;
            _stripeReferenceService = stripeReferenceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var userId = HttpContext.GetUserId();

                if (!await _stripeReferenceService.ReferenceExistsAsync(userId))
                {
                    return this.NotFound("Stripe reference not found.");
                }

                var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

                var customer = await _stripeCustomerService.FindCustomerAsync(customerId);

                return this.Ok(customer);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

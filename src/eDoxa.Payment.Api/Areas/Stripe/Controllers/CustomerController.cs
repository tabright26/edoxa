// Filename: CustomerController.cs
// Date Created: 2019-10-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Payment.Api.Areas.Stripe.Responses;
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
        private readonly IMapper _mapper;

        public CustomerController(IStripeCustomerService stripeCustomerService, IStripeReferenceService stripeReferenceService, IMapper mapper)
        {
            _stripeCustomerService = stripeCustomerService;
            _stripeReferenceService = stripeReferenceService;
            _mapper = mapper;
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

                return this.Ok(_mapper.Map<CustomerResponse>(customer));
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

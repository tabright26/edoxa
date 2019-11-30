// Filename: StripeCustomerController.cs
// Date Created: 2019-10-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.Responses;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Stripe;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Payment.Api.Areas.Stripe.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/stripe/customer")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeCustomerController : ControllerBase
    {
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeReferenceService _stripeReferenceService;
        private readonly IMapper _mapper;

        public StripeCustomerController(IStripeCustomerService stripeCustomerService, IStripeReferenceService stripeReferenceService, IMapper mapper)
        {
            _stripeCustomerService = stripeCustomerService;
            _stripeReferenceService = stripeReferenceService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find customer.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeCustomerResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
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

                return this.Ok(_mapper.Map<StripeCustomerResponse>(customer));
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

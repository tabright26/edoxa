// Filename: StripeCustomerPaymentMethodDefaultController.cs
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

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Payment.Api.Areas.Stripe.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/stripe/customer/payment-methods/{paymentMethodId}/default")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeCustomerPaymentMethodDefaultController : ControllerBase
    {
        private readonly IStripeReferenceService _stripeReferenceService;
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IMapper _mapper;

        public StripeCustomerPaymentMethodDefaultController(
            IStripeReferenceService stripeReferenceService,
            IStripeCustomerService stripeCustomerService,
            IMapper mapper
        )
        {
            _stripeReferenceService = stripeReferenceService;
            _stripeCustomerService = stripeCustomerService;
            _mapper = mapper;
        }

        [HttpPut]
        [SwaggerOperation("Set payment method as default.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeCustomerResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PutAsync(string paymentMethodId)
        {
            var userId = HttpContext.GetUserId();

            if (!await _stripeReferenceService.ReferenceExistsAsync(userId))
            {
                return this.NotFound("Stripe reference not found.");
            }

            var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

            var customer = await _stripeCustomerService.SetDefaultPaymentMethodAsync(customerId, paymentMethodId);

            return this.Ok(_mapper.Map<StripeCustomerResponse>(customer));
        }
    }
}

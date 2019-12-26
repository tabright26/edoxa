// Filename: StripePaymentMethodsController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Payment.Dtos;
using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Payment.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/stripe/payment-methods")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripePaymentMethodsController : ControllerBase
    {
        private readonly IStripePaymentMethodService _stripePaymentMethodService;
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;

        public StripePaymentMethodsController(
            IStripePaymentMethodService stripePaymentMethodService,
            IStripeCustomerService stripeCustomerService,
            IStripeService stripeService,
            IMapper mapper
        )
        {
            _stripePaymentMethodService = stripePaymentMethodService;
            _stripeCustomerService = stripeCustomerService;
            _stripeService = stripeService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Fetch payment methods.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripePaymentMethodDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync([FromQuery] string type)
        {
            var userId = HttpContext.GetUserId();

            if (!await _stripeService.UserExistsAsync(userId))
            {
                return this.NotFound("Stripe reference not found.");
            }

            var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

            var paymentMethods = await _stripePaymentMethodService.FetchPaymentMethodsAsync(customerId, type);

            if (!paymentMethods.Any())
            {
                return this.NoContent();
            }

            return this.Ok(_mapper.Map<IEnumerable<StripePaymentMethodDto>>(paymentMethods));
        }

        [HttpPut("{paymentMethodId}")]
        [SwaggerOperation("Update payment methods.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripePaymentMethodDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PutAsync(string paymentMethodId, [FromBody] UpdateStripePaymentMethodRequest request)
        {
            var userId = HttpContext.GetUserId();

            if (!await _stripeService.UserExistsAsync(userId))
            {
                return this.NotFound("Stripe reference not found.");
            }

            var paymentMethod = await _stripePaymentMethodService.UpdatePaymentMethodAsync(paymentMethodId, request.ExpMonth, request.ExpYear);

            return this.Ok(_mapper.Map<StripePaymentMethodDto>(paymentMethod));
        }
    }
}

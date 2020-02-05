// Filename: PaymentMethodsController.cs
// Date Created: 2020-01-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Payment.Dtos;
using eDoxa.Grpc.Protos.Payment.Requests;
using eDoxa.Payment.Api.Application.Stripe.Extensions;
using eDoxa.Payment.Api.Application.Stripe.Services.Abstractions;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Payment.Api.Controllers.Stripe
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/stripe/payment-methods")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class PaymentMethodsController : ControllerBase
    {
        private readonly IStripePaymentMethodService _stripePaymentMethodService;
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IMapper _mapper;

        public PaymentMethodsController(IStripePaymentMethodService stripePaymentMethodService, IStripeCustomerService stripeCustomerService, IMapper mapper)
        {
            _stripePaymentMethodService = stripePaymentMethodService;
            _stripeCustomerService = stripeCustomerService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Fetch payment methods.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripePaymentMethodDto[]))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> FetchPaymentMethodsAsync()
        {
            var customerId = HttpContext.GetStripeCustomertId();

            var paymentMethods = await _stripePaymentMethodService.FetchPaymentMethodsAsync(customerId);

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
        public async Task<IActionResult> UpdatePaymentMethodAsync(string paymentMethodId, [FromBody] UpdateStripePaymentMethodRequest request)
        {
            var paymentMethod = await _stripePaymentMethodService.UpdatePaymentMethodAsync(paymentMethodId, request.ExpMonth, request.ExpYear);

            return this.Ok(_mapper.Map<StripePaymentMethodDto>(paymentMethod));
        }

        [HttpPost("{paymentMethodId}/attach")]
        [SwaggerOperation("Attach a payment method.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripePaymentMethodDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> AttachPaymentMethodAsync(string paymentMethodId, [FromBody] AttachStripePaymentMethodRequest request)
        {
            var customerId = HttpContext.GetStripeCustomertId();

            var result = await _stripePaymentMethodService.AttachPaymentMethodAsync(paymentMethodId, customerId, request.DefaultPaymentMethod);

            if (result.IsValid)
            {
                return this.Ok(_mapper.Map<StripePaymentMethodDto>(result.Response));
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("{paymentMethodId}/detach")]
        [SwaggerOperation("Detach a payment method.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripePaymentMethodDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DetachPaymentMethodAsync(string paymentMethodId)
        {
            var paymentMethod = await _stripePaymentMethodService.DetachPaymentMethodAsync(paymentMethodId);

            return this.Ok(_mapper.Map<StripePaymentMethodDto>(paymentMethod));
        }

        [HttpPut("{paymentMethodId}/default")]
        [SwaggerOperation("Set payment method as default.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeCustomerDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> SetDefaultPaymentMethodAsync(string paymentMethodId)
        {
            var customerId = HttpContext.GetStripeCustomertId();

            var customer = await _stripeCustomerService.SetDefaultPaymentMethodAsync(customerId, paymentMethodId);

            return this.Ok(_mapper.Map<StripeCustomerDto>(customer));
        }
    }
}

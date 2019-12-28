﻿// Filename: StripeCustomerPaymentMethodDefaultController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Payment.Dtos;
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
    [Route("api/stripe/customer/payment-methods/{paymentMethodId}/default")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeCustomerPaymentMethodDefaultController : ControllerBase
    {
        private readonly IStripeService _stripeService;
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IMapper _mapper;

        public StripeCustomerPaymentMethodDefaultController(
            IStripeService stripeService,
            IStripeCustomerService stripeCustomerService,
            IMapper mapper
        )
        {
            _stripeService = stripeService;
            _stripeCustomerService = stripeCustomerService;
            _mapper = mapper;
        }

        [HttpPut]
        [SwaggerOperation("Set payment method as default.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeCustomerDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PutAsync(string paymentMethodId)
        {
            var userId = HttpContext.GetUserId();

            if (!await _stripeService.UserExistsAsync(userId))
            {
                return this.NotFound("Stripe reference not found.");
            }

            var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

            var customer = await _stripeCustomerService.SetDefaultPaymentMethodAsync(customerId, paymentMethodId);

            return this.Ok(_mapper.Map<StripeCustomerDto>(customer));
        }
    }
}
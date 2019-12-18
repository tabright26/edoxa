﻿// Filename: StripeCustomerController.cs
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
    [Route("api/stripe/customer")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeCustomerController : ControllerBase
    {
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;

        public StripeCustomerController(IStripeCustomerService stripeCustomerService, IStripeService stripeService, IMapper mapper)
        {
            _stripeCustomerService = stripeCustomerService;
            _stripeService = stripeService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find customer.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeCustomerDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync()
        {
            var userId = HttpContext.GetUserId();

            if (!await _stripeService.UserExistsAsync(userId))
            {
                return this.NotFound("Stripe reference not found.");
            }

            var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

            var customer = await _stripeCustomerService.FindCustomerAsync(customerId);

            return this.Ok(_mapper.Map<StripeCustomerDto>(customer));
        }
    }
}

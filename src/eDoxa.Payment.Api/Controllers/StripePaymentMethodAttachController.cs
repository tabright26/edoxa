// Filename: StripePaymentMethodAttachController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
    [Route("api/stripe/payment-methods/{paymentMethodId}/attach")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripePaymentMethodAttachController : ControllerBase
    {
        private readonly IStripePaymentMethodService _stripePaymentMethodService;
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;

        public StripePaymentMethodAttachController(
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

        [HttpPost]
        [SwaggerOperation("Attach a payment method.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripePaymentMethodDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync(string paymentMethodId, [FromBody] AttachStripePaymentMethodRequest request)
        {
            var userId = HttpContext.GetUserId();

            if (!await _stripeService.UserExistsAsync(userId))
            {
                return this.NotFound("Stripe reference not found.");
            }

            var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

            var paymentMethod = await _stripePaymentMethodService.AttachPaymentMethodAsync(paymentMethodId, customerId, request.DefaultPaymentMethod);

            return this.Ok(_mapper.Map<StripePaymentMethodDto>(paymentMethod));
        }
    }
}

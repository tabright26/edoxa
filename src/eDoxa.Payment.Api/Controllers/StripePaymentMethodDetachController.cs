// Filename: StripePaymentMethodDetachController.cs
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
    [Route("api/stripe/payment-methods/{paymentMethodId}/detach")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripePaymentMethodDetachController : ControllerBase
    {
        private readonly IStripePaymentMethodService _stripePaymentMethodService;
        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;

        public StripePaymentMethodDetachController(
            IStripePaymentMethodService stripePaymentMethodService,
            IStripeService stripeService,
            IMapper mapper
        )
        {
            _stripePaymentMethodService = stripePaymentMethodService;
            _stripeService = stripeService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation("Detach a payment method.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripePaymentMethodDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync(string paymentMethodId)
        {
            var userId = HttpContext.GetUserId();

            if (!await _stripeService.UserExistsAsync(userId))
            {
                return this.NotFound("Stripe reference not found.");
            }

            var paymentMethod = await _stripePaymentMethodService.DetachPaymentMethodAsync(paymentMethodId);

            return this.Ok(_mapper.Map<StripePaymentMethodDto>(paymentMethod));
        }
    }
}

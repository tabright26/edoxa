// Filename: PaymentMethodAttachController.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Payment.Api.Areas.Stripe.Requests;
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
    [Route("api/stripe/payment-methods/{paymentMethodId}/attach")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class PaymentMethodAttachController : ControllerBase
    {
        private readonly IStripePaymentMethodService _stripePaymentMethodService;
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IStripeReferenceService _stripeReferenceService;
        private readonly IMapper _mapper;

        public PaymentMethodAttachController(
            IStripePaymentMethodService stripePaymentMethodService,
            IStripeCustomerService stripeCustomerService,
            IStripeReferenceService stripeReferenceService,
            IMapper mapper
        )
        {
            _stripePaymentMethodService = stripePaymentMethodService;
            _stripeCustomerService = stripeCustomerService;
            _stripeReferenceService = stripeReferenceService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(string paymentMethodId, [FromBody] PaymentMethodAttachPostRequest request)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                if (!await _stripeReferenceService.ReferenceExistsAsync(userId))
                {
                    return this.NotFound("Stripe reference not found.");
                }

                var customerId = await _stripeCustomerService.GetCustomerIdAsync(userId);

                var paymentMethod = await _stripePaymentMethodService.AttachPaymentMethodAsync(paymentMethodId, customerId, request.DefaultPaymentMethod);

                return this.Ok(_mapper.Map<PaymentMethodResponse>(paymentMethod));
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

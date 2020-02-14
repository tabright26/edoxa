// Filename: StripeCustomerController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Grpc.Protos.Payment.Dtos;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Stripe.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Payment.Api.Controllers.Stripe
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/stripe/customer")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class CustomerController : ControllerBase
    {
        private readonly IStripeCustomerService _stripeCustomerService;
        private readonly IMapper _mapper;

        public CustomerController(IStripeCustomerService stripeCustomerService, IMapper mapper)
        {
            _stripeCustomerService = stripeCustomerService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find customer.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeCustomerDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> FetchCustomerAsync()
        {
            var customerId = HttpContext.GetStripeCustomerId();

            var customer = await _stripeCustomerService.FindCustomerAsync(customerId);

            return this.Ok(_mapper.Map<StripeCustomerDto>(customer));
        }
    }
}

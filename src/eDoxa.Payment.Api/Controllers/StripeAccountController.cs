// Filename: StripeAccountController.cs
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
    [Route("api/stripe/account")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeAccountController : ControllerBase
    {
        private readonly IStripeAccountService _stripeAccountService;
        private readonly IStripeReferenceService _stripeReferenceService;
        private readonly IMapper _mapper;

        public StripeAccountController(IStripeAccountService stripeAccountService, IStripeReferenceService stripeReferenceService, IMapper mapper)
        {
            _stripeAccountService = stripeAccountService;
            _stripeReferenceService = stripeReferenceService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find account.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeAccountDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAsync()
        {
            var userId = HttpContext.GetUserId();

            if (!await _stripeReferenceService.ReferenceExistsAsync(userId))
            {
                return this.NotFound("Stripe reference not found.");
            }

            var accountId = await _stripeAccountService.GetAccountIdAsync(userId);

            var account = await _stripeAccountService.GetAccountAsync(accountId);

            return this.Ok(_mapper.Map<StripeAccountDto>(account));
        }
    }
}

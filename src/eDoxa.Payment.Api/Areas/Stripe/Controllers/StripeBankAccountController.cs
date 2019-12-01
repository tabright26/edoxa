// Filename: StripeBankAccountController.cs
// Date Created: 2019-10-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.Requests;
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
    [Route("api/stripe/bank-account")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class StripeBankAccountController : ControllerBase
    {
        private readonly IStripeExternalAccountService _externalAccountService;
        private readonly IStripeAccountService _stripeAccountService;
        private readonly IStripeReferenceService _stripeReferenceService;
        private readonly IMapper _mapper;

        public StripeBankAccountController(
            IStripeExternalAccountService externalAccountService,
            IStripeAccountService stripeAccountService,
            IStripeReferenceService stripeReferenceService,
            IMapper mapper
        )
        {
            _externalAccountService = externalAccountService;
            _stripeAccountService = stripeAccountService;
            _stripeReferenceService = stripeReferenceService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation("Find bank account.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeBankAccountResponse))]
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

            var bankAccount = await _externalAccountService.FindBankAccountAsync(accountId);

            if (bankAccount == null)
            {
                return this.NotFound("Bank account not found.");
            }

            return this.Ok(_mapper.Map<StripeBankAccountResponse>(bankAccount));
        }

        [HttpPost]
        [SwaggerOperation("Update bank account.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StripeBankAccountResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync([FromBody] StripeBankAccountPostRequest request)
        {
            var userId = HttpContext.GetUserId();

            if (!await _stripeReferenceService.ReferenceExistsAsync(userId))
            {
                return this.NotFound("Stripe reference not found.");
            }

            var accountId = await _stripeAccountService.GetAccountIdAsync(userId);

            var bankAccount = await _externalAccountService.UpdateBankAccountAsync(accountId, request.Token);

            return this.Ok(_mapper.Map<StripeBankAccountResponse>(bankAccount));
        }
    }
}

// Filename: BankAccountController.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.Areas.Stripe.Requests;
using eDoxa.Payment.Api.Extensions;
using eDoxa.Payment.Domain.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/stripe/bank-account")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class BankAccountController : ControllerBase
    {
        private readonly IStripeExternalAccountService _externalAccountService;
        private readonly IStripeAccountService _stripeAccountService;
        private readonly IStripeReferenceService _stripeReferenceService;

        public BankAccountController(
            IStripeExternalAccountService externalAccountService,
            IStripeAccountService stripeAccountService,
            IStripeReferenceService stripeReferenceService
        )
        {
            _externalAccountService = externalAccountService;
            _stripeAccountService = stripeAccountService;
            _stripeReferenceService = stripeReferenceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
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

                return this.Ok(bankAccount);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BankAccountPostRequest request)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                if (!await _stripeReferenceService.ReferenceExistsAsync(userId))
                {
                    return this.NotFound("Stripe reference not found.");
                }

                var accountId = await _stripeAccountService.GetAccountIdAsync(userId);

                var bankAccount = await _externalAccountService.UpdateBankAccountAsync(accountId, request.Token);

                return this.Ok(bankAccount);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

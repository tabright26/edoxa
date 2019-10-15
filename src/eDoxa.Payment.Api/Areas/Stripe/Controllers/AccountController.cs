// Filename: AccountController.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

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
    [Route("api/stripe/account")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class AccountController : ControllerBase
    {
        private readonly IStripeAccountService _stripeAccountService;
        private readonly IStripeReferenceService _stripeReferenceService;

        public AccountController(IStripeAccountService stripeAccountService, IStripeReferenceService stripeReferenceService)
        {
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

                var account = await _stripeAccountService.GetAccountAsync(accountId);

                return this.Ok(account);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

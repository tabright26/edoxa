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
        private readonly IStripeAccountService _stripeAccountService;

        public BankAccountController(IStripeAccountService stripeAccountService)
        {
            _stripeAccountService = stripeAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var userId = HttpContext.GetUserId();

                var accountId = await _stripeAccountService.FindAccountIdAsync(userId);

                if (accountId == null)
                {
                    return this.NotFound("User not found.");
                }

                var bankAccount = await _stripeAccountService.FindBankAccountAsync(accountId);

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

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] BankAccountPutRequest request)
        {
            try
            {
                var userId = HttpContext.GetUserId();

                var accountId = await _stripeAccountService.FindAccountIdAsync(userId);

                if (accountId == null)
                {
                    return this.NotFound("User not found.");
                }

                var bankAccount = await _stripeAccountService.UpdateBankAccountAsync(accountId, request.Token);

                return this.Ok(bankAccount);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

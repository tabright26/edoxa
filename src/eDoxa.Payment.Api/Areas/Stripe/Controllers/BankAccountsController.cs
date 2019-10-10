// Filename: BankAccountsController.cs
// Date Created: 2019-10-08
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
    [Route("api/stripe/bank-accounts")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class BankAccountsController : ControllerBase
    {
        private readonly IStripeExternalAccountService _externalAccountService;
        private readonly IStripeAccountService _stripeAccountService;

        public BankAccountsController(IStripeExternalAccountService externalAccountService, IStripeAccountService stripeAccountService)
        {
            _externalAccountService = externalAccountService;
            _stripeAccountService = stripeAccountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var userId = HttpContext.GetUserId();

            var accountId = await _stripeAccountService.FindAccountIdAsync(userId);

            if (accountId == null)
            {
                return this.NotFound("User not found.");
            }

            try
            {
                var bankAccounts = await _externalAccountService.FetchBankAccountsAsync(accountId);

                return this.Ok(bankAccounts);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BankAccountPostRequest request)
        {
            var userId = HttpContext.GetUserId();

            var accountId = await _stripeAccountService.FindAccountIdAsync(userId);

            if (accountId == null)
            {
                return this.NotFound("User not found.");
            }

            try
            {
                var bankAccount = await _externalAccountService.ChangeBankAccountAsync(accountId, request.Token);

                return this.Ok(bankAccount);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }

        //[HttpDelete("{bankAccountId}")]
        //public async Task<IActionResult> DeleteAsync(string bankAccountId)
        //{
        //    var userId = HttpContext.GetUserId();

        //    var accountId = await _stripeAccountService.FindAccountIdAsync(userId);

        //    if (accountId == null)
        //    {
        //        return this.NotFound("User not found.");
        //    }

        //    try
        //    {
        //        var bankAccount = await _externalAccountService.DeleteBankAccountAsync(accountId, bankAccountId);

        //        return this.Ok(bankAccount);
        //    }
        //    catch (StripeException exception)
        //    {
        //        return this.BadRequest(exception.StripeResponse);
        //    }
        //}
    }
}

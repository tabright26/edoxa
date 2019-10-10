// Filename: AccountController.cs
// Date Created: 2019-10-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.Areas.Stripe.Requests;
using eDoxa.Payment.Api.Areas.Stripe.Services;
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
    [Route("api/stripe/persons")]
    [ApiExplorerSettings(GroupName = "Stripe")]
    public sealed class PersonsController : ControllerBase
    {
        private readonly IStripeAccountService _stripeAccountService;
        private readonly IStripePersonService _stripePersonService;

        public PersonsController(IStripeAccountService stripeAccountService, IStripePersonService stripePersonService)
        {
            _stripeAccountService = stripeAccountService;
            _stripePersonService = stripePersonService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PersonPostRequest request)
        {
            var userId = HttpContext.GetUserId();

            var accountId = await _stripeAccountService.FindAccountIdAsync(userId);

            if (accountId == null)
            {
                return this.NotFound("User not found.");
            }

            try
            {
                var bankAccounts = await _stripePersonService.CreatePersonAsync(accountId, request.Token);

                return this.Ok(bankAccounts);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }

        [HttpPut("{personId}")]
        public async Task<IActionResult> PutAsync(string personId, [FromBody] PersonPutRequest request)
        {
            var userId = HttpContext.GetUserId();

            var accountId = await _stripeAccountService.FindAccountIdAsync(userId);

            if (accountId == null)
            {
                return this.NotFound("User not found.");
            }

            try
            {
                var bankAccounts = await _stripePersonService.UpdatePersonAsync(accountId, personId, request.Token);

                return this.Ok(bankAccounts);
            }
            catch (StripeException exception)
            {
                return this.BadRequest(exception.StripeResponse);
            }
        }
    }
}

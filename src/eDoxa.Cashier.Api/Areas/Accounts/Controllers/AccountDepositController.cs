// Filename: AccountDepositController.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.Services;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Accounts.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/account/deposit")]
    [ApiExplorerSettings(GroupName = "Account")]
    public sealed class AccountDepositController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountDepositController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        ///     Deposit currency on the account.
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync([FromBody] AccountDepositPostRequest request)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.GetUserId();

                var customerId = HttpContext.GetCustomerId()!;

                var account = await _accountService.FindUserAccountAsync(userId);

                if (account == null)
                {
                    return this.NotFound("User's account not found.");
                }

                var result = await _accountService.DepositAsync(account, request.Currency.Format(request.Amount), customerId);

                if (result.IsValid)
                {
                    return this.Ok("Processing the deposit transaction...");
                }

                result.AddToModelState(ModelState, null);
            }

            return this.BadRequest(ModelState);
        }
    }
}

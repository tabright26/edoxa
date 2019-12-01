// Filename: AccountWithdrawalController.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Responses;
using eDoxa.Seedwork.Application.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Accounts.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/account/withdrawal/{currency}")]
    [ApiExplorerSettings(GroupName = "Account")]
    public sealed class AccountWithdrawalController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IBundlesService _bundlesService;
        private readonly IMapper _mapper;

        public AccountWithdrawalController(IAccountService accountService, IBundlesService bundlesService, IMapper mapper)
        {
            _accountService = accountService;
            _bundlesService = bundlesService;
            _mapper = mapper;
        }

        [HttpPost]
        [SwaggerOperation("Withdrawal money from the account.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> PostAsync(Currency currency, [FromBody] decimal amount)
        {
            var userId = HttpContext.GetUserId();

            var email = HttpContext.GetEmail();

            var account = await _accountService.FindUserAccountAsync(userId);

            if (account == null)
            {
                return this.NotFound("User's account not found.");
            }

            var result = await _accountService.WithdrawalAsync(account, currency.Format(amount), email);

            if (result.IsValid)
            {
                return this.Ok("Processing the deposit transaction...");
            }

            result.AddToModelState(ModelState);

            return this.BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpGet("bundles")]
        [SwaggerOperation("Get bundles by currency.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BundleResponse[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult Get(Currency currency)
        {
            if (currency == Currency.Money)
            {
                return this.Ok(_mapper.Map<IEnumerable<BundleResponse>>(_bundlesService.FetchWithdrawalMoneyBundles()));
            }

            return this.BadRequest("Invalid or unsuported currency.");
        }
    }
}

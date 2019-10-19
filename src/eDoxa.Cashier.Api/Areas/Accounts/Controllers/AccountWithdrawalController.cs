﻿// Filename: AccountWithdrawalController.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.Areas.Accounts.Responses;
using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

namespace eDoxa.Cashier.Api.Areas.Accounts.Controllers
{
    [Authorize]
    [ApiController]
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

        /// <summary>
        ///     Withdrawal money from the account.
        /// </summary>
        [HttpPost]
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

            result.AddToModelState(ModelState, null);

            return this.ValidationProblem(ModelState);
        }

        [HttpGet("bundles")]
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

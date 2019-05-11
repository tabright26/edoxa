// Filename: BankAccountController.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/bank-account")]
    public class BankAccountController : ControllerBase
    {
        private readonly BankAccountService _accountService;
        private readonly IMediator _mediator;
        private readonly IUserInfoService _userInfoService;

        public BankAccountController(IMediator mediator, BankAccountService accountService, IUserInfoService userInfoService)
        {
            _mediator = mediator;
            _accountService = accountService;
            _userInfoService = userInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> FindBankAccount()
        {
            var customerId = new CustomerId(_userInfoService.CustomerId);

            var bankAccountId = _userInfoService.BankAccountId;

            if (bankAccountId == null)
            {
                return this.NotFound();
            }

            var bankAccount = await _accountService.GetAsync(customerId.ToString(), bankAccountId);

            if (bankAccount == null)
            {
                return this.NotFound();
            }

            return this.Ok(bankAccount);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccountCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBankAccount()
        {
            return await _mediator.SendCommandAsync(new DeleteBankAccountCommand());
        }
    }
}
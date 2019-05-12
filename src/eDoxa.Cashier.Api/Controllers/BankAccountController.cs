// Filename: BankAccountController.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/bank-account")]
    public sealed class BankAccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBankAccountQueries _bankAccountQueries;
        private readonly IUserInfoService _userInfoService;

        public BankAccountController(IMediator mediator, IBankAccountQueries bankAccountQueries, IUserInfoService userInfoService)
        {
            _mediator = mediator;
            _bankAccountQueries = bankAccountQueries;
            _userInfoService = userInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> FindBankAccount()
        {
            var customerId = _userInfoService.CustomerId;

            var bankAccount = await _bankAccountQueries.FindUserBankAccountAsync(new CustomerId(customerId));

            return bankAccount
                .Select(this.Ok)
                .Cast<IActionResult>()
                .DefaultIfEmpty(this.NotFound("User don't have a bank account."))
                .Single();
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

        [HttpPatch("verify")]
        public async Task<IActionResult> VerifyBankAccount([FromBody] VerifyBankAccountCommand command)
        {
            return await _mediator.SendCommandAsync(command);
        }
    }
}
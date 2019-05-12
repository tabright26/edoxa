// Filename: CreateBankAccountCommandHandler.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class CreateBankAccountCommandHandler : ICommandHandler<CreateBankAccountCommand, IActionResult>
    {
        private readonly IStripeService _stripeService;
        private readonly IUserInfoService _userInfoService;

        public CreateBankAccountCommandHandler(IUserInfoService userInfoService, IStripeService stripeService)
        {
            _userInfoService = userInfoService;
            _stripeService = stripeService;
        }

        public async Task<IActionResult> Handle(CreateBankAccountCommand command, CancellationToken cancellationToken)
        {
            var customerId = new CustomerId(_userInfoService.CustomerId);

            var either = await _stripeService.CreateBankAccountAsync(customerId, command.SourceToken, cancellationToken);

            return either.Match<IActionResult>(
                result => new BadRequestObjectResult(result.ErrorMessage),
                bankAccount => new OkObjectResult("The bank account has been added.")
            );
        }
    }
}
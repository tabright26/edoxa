// Filename: VerifyBankAccountCommandHandler.cs
// Date Created: 2019-05-11
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
    internal sealed class VerifyAccountCommandHandler : ICommandHandler<VerifyAccountCommand, IActionResult>
    {
        private readonly IStripeService _stripeService;
        private readonly IUserInfoService _userInfoService;

        public VerifyAccountCommandHandler(IStripeService stripeService, IUserInfoService userInfoService)
        {
            _stripeService = stripeService;
            _userInfoService = userInfoService;
        }

        public async Task<IActionResult> Handle(VerifyAccountCommand command, CancellationToken cancellationToken)
        {
            var accountId = new StripeAccountId(_userInfoService.StripeAccountId);

            if (!command.TermsOfService)
            {
                return new BadRequestObjectResult("You must agree to the Stripe terms of service to verify the account.");
            }

            await _stripeService.VerifyAccountAsync(accountId, command.Line1, command.Line2, command.City, command.State, command.PostalCode, cancellationToken);

            return new OkObjectResult("Stripe account verified.");
        }
    }
}
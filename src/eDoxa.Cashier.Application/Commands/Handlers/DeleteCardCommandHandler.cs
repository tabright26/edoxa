// Filename: DeleteCardCommandHandler.cs
// Date Created: 2019-05-06
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

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DeleteCardCommandHandler : ICommandHandler<DeleteCardCommand, IActionResult>
    {
        private readonly IStripeService _stripeService;
        private readonly IUserInfoService _userInfoService;

        public DeleteCardCommandHandler(IUserInfoService userInfoService, IStripeService stripeService)
        {
            _userInfoService = userInfoService;
            _stripeService = stripeService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var customerId = new StripeCustomerId(_userInfoService.StripeCustomerId);

            var either = await _stripeService.DeleteCardAsync(customerId, command.StripeCardId, cancellationToken);

            return either.Match<IActionResult>(
                result => new BadRequestObjectResult(result.ErrorMessage),
                card => new OkObjectResult("The card has been removed.")
            );
        }
    }
}
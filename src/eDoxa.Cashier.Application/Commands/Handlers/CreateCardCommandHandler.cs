// Filename: CreateCardCommandHandler.cs
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
    internal sealed class CreateCardCommandHandler : ICommandHandler<CreateCardCommand, IActionResult>
    {
        private readonly IStripeService _stripeService;
        private readonly IUserInfoService _userInfoService;

        public CreateCardCommandHandler(IUserInfoService userInfoService, IStripeService stripeService)
        {
            _userInfoService = userInfoService;
            _stripeService = stripeService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] CreateCardCommand command, CancellationToken cancellationToken)
        {
            var customerId = CustomerId.Parse(_userInfoService.CustomerId);

            await _stripeService.CreateCardAsync(customerId, command.SourceToken, command.DefaultSource, cancellationToken);

            return new OkResult();
        }
    }
}
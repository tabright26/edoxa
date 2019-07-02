﻿// Filename: CreateCardCommandHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Stripe.Abstractions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Api.Application.Commands.Handlers
{
    public sealed class CreateCardCommandHandler : AsyncCommandHandler<CreateCardCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStripeService _stripeService;
        private readonly IUserQuery _userQuery;

        public CreateCardCommandHandler(IHttpContextAccessor httpContextAccessor, IStripeService stripeService, IUserQuery userQuery)
        {
            _httpContextAccessor = httpContextAccessor;
            _stripeService = stripeService;
            _userQuery = userQuery;
        }

        protected override async Task Handle(CreateCardCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userQuery.FindUserAsync(userId);

            await _stripeService.CreateCardAsync(user.GetCustomerId(), command.SourceToken, cancellationToken);
        }
    }
}

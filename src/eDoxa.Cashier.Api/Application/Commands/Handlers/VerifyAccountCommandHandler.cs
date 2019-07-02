// Filename: VerifyAccountCommandHandler.cs
// Date Created: 2019-06-08
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
    public sealed class VerifyAccountCommandHandler : AsyncCommandHandler<VerifyAccountCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStripeService _stripeService;
        private readonly IUserQuery _userQuery;

        public VerifyAccountCommandHandler(IStripeService stripeService, IHttpContextAccessor httpContextAccessor, IUserQuery userQuery)
        {
            _stripeService = stripeService;
            _httpContextAccessor = httpContextAccessor;
            _userQuery = userQuery;
        }

        protected override async Task Handle(VerifyAccountCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userQuery.FindUserAsync(userId);

            await _stripeService.VerifyAccountAsync(
                user.GetConnectAccountId(),
                command.Line1,
                command.Line2,
                command.City,
                command.State,
                command.PostalCode,
                cancellationToken
            );
        }
    }
}

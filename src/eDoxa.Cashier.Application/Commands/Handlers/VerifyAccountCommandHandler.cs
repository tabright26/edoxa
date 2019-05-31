// Filename: VerifyAccountCommandHandler.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Services.Extensions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Extensions;
using eDoxa.Stripe.Abstractions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class VerifyAccountCommandHandler : AsyncCommandHandler<VerifyAccountCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public VerifyAccountCommandHandler(IStripeService stripeService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _stripeService = stripeService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        protected override async Task Handle(VerifyAccountCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var user = await _userRepository.GetUserAsNoTrackingAsync(userId);

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

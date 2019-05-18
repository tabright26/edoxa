// Filename: UpdateCardDefaultCommandHandler.cs
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
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class UpdateCardDefaultCommandHandler : ICommandHandler<UpdateCardDefaultCommand, Either>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IStripeService _stripeService;

        public UpdateCardDefaultCommandHandler(ICashierHttpContext cashierHttpContext, IStripeService stripeService)
        {
            _cashierHttpContext = cashierHttpContext;
            _stripeService = stripeService;
        }

        [ItemCanBeNull]
        public async Task<Either> Handle([NotNull] UpdateCardDefaultCommand command, CancellationToken cancellationToken)
        {
            var customerId = _cashierHttpContext.StripeCustomerId;

            await _stripeService.UpdateCardDefaultAsync(customerId, command.StripeCardId, cancellationToken);

            return new Success("The card has been updated as default.");
        }
    }
}
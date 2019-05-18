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
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class CreateCardCommandHandler : ICommandHandler<CreateCardCommand, Either>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IStripeService _stripeService;

        public CreateCardCommandHandler(ICashierHttpContext cashierHttpContext, IStripeService stripeService)
        {
            _cashierHttpContext = cashierHttpContext;
            _stripeService = stripeService;
        }

        [ItemNotNull]
        public async Task<Either> Handle([NotNull] CreateCardCommand command, CancellationToken cancellationToken)
        {
            var customerId = _cashierHttpContext.StripeCustomerId;

            await _stripeService.CreateCardAsync(customerId, command.SourceToken, cancellationToken);

            return new Success("The card has been added.");
        }
    }
}
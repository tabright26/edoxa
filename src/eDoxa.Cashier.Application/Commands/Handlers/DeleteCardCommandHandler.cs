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
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DeleteCardCommandHandler : ICommandHandler<DeleteCardCommand, Either<ValidationError, CommandResult>>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IStripeService _stripeService;

        public DeleteCardCommandHandler(ICashierHttpContext cashierHttpContext, IStripeService stripeService)
        {
            _cashierHttpContext = cashierHttpContext;
            _stripeService = stripeService;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, CommandResult>> Handle([NotNull] DeleteCardCommand command, CancellationToken cancellationToken)
        {
            await _stripeService.DeleteCardAsync(_cashierHttpContext.StripeCustomerId, command.StripeCardId, cancellationToken);

            return new CommandResult("The card has been removed.");
        }
    }
}

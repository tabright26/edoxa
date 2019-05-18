// Filename: VerifyAccountCommandHandler.cs
// Date Created: 2019-05-13
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

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class VerifyAccountCommandHandler : ICommandHandler<VerifyAccountCommand, Either<ValidationError, CommandResult>>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IStripeService _stripeService;

        public VerifyAccountCommandHandler(IStripeService stripeService, ICashierHttpContext cashierHttpContext)
        {
            _stripeService = stripeService;
            _cashierHttpContext = cashierHttpContext;
        }

        public async Task<Either<ValidationError, CommandResult>> Handle(VerifyAccountCommand command, CancellationToken cancellationToken)
        {
            await _stripeService.VerifyAccountAsync(_cashierHttpContext.StripeAccountId, command.Line1, command.Line2, command.City, command.State, command.PostalCode, cancellationToken);

            return new CommandResult("Stripe account verified.");
        }
    }
}
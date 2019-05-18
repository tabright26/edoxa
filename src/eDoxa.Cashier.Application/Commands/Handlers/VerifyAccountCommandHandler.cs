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

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class VerifyAccountCommandHandler : ICommandHandler<VerifyAccountCommand, Either>
    {
        private readonly ICashierSecurity _cashierSecurity;
        private readonly IStripeService _stripeService;

        public VerifyAccountCommandHandler(IStripeService stripeService, ICashierSecurity cashierSecurity)
        {
            _stripeService = stripeService;
            _cashierSecurity = cashierSecurity;
        }

        public async Task<Either> Handle(VerifyAccountCommand command, CancellationToken cancellationToken)
        {
            var accountId = _cashierSecurity.StripeAccountId;

            if (!command.TermsOfService)
            {
                return new Failure("You must agree to the Stripe terms of service to verify the account.");
            }

            await _stripeService.VerifyAccountAsync(accountId, command.Line1, command.Line2, command.City, command.State, command.PostalCode,
                cancellationToken);

            return new Success("Stripe account verified.");
        }
    }
}
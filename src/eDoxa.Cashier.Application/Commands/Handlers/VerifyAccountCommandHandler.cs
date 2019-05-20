// Filename: VerifyAccountCommandHandler.cs
// Date Created: 2019-05-19
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
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class VerifyAccountCommandHandler : ICommandHandler<VerifyAccountCommand, Either<ValidationError, CommandResult>>
    {
        private readonly ICashierHttpContext _cashierHttpContext;
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public VerifyAccountCommandHandler(IStripeService stripeService, ICashierHttpContext cashierHttpContext, IUserRepository userRepository)
        {
            _stripeService = stripeService;
            _cashierHttpContext = cashierHttpContext;
            _userRepository = userRepository;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, CommandResult>> Handle([NotNull] VerifyAccountCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindUserAsNoTrackingAsync(_cashierHttpContext.UserId);

            await _stripeService.VerifyAccountAsync(
                user.AccountId,
                command.Line1,
                command.Line2,
                command.City,
                command.State,
                command.PostalCode,
                cancellationToken
            );

            return new CommandResult("Stripe account verified.");
        }
    }
}

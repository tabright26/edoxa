// Filename: InitializeServiceCommandHandler.cs
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
using eDoxa.Cashier.Services.Stripe.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class CreateUserCommandHandler : AsyncCommandHandler<CreateUserCommand>
    {
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IStripeService stripeService, IUserRepository userRepository)
        {
            _stripeService = stripeService;
            _userRepository = userRepository;
        }

        protected override async Task Handle([NotNull] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var accountId = await _stripeService.CreateAccountAsync(
                command.UserId,
                command.Email,
                command.FirstName,
                command.LastName,
                command.Year,
                command.Month,
                command.Day,
                cancellationToken
            );

            var customerId = await _stripeService.CreateCustomerAsync(command.UserId, accountId, command.Email, cancellationToken);

            _userRepository.Create(command.UserId, accountId, customerId);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}

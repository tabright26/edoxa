// Filename: CreateUserCommandHandler.cs
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

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Stripe.Abstractions;

namespace eDoxa.Cashier.Api.Application.Commands.Handlers
{
    public sealed class CreateUserCommandHandler : AsyncCommandHandler<CreateUserCommand>
    {
        private readonly IStripeService _stripeService;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IStripeService stripeService, IUserRepository userRepository)
        {
            _stripeService = stripeService;
            _userRepository = userRepository;
        }

        protected override async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var connectAccountId = await _stripeService.CreateAccountAsync(
                command.UserId,
                command.Email,
                command.FirstName,
                command.LastName,
                command.Year,
                command.Month,
                command.Day,
                cancellationToken
            );

            var customerId = await _stripeService.CreateCustomerAsync(command.UserId, connectAccountId, command.Email, cancellationToken);

            var user = new User(command.UserId, connectAccountId.ToString(), customerId.ToString());

            _userRepository.Create(user);

            await _userRepository.CommitAsync(cancellationToken);
        }
    }
}

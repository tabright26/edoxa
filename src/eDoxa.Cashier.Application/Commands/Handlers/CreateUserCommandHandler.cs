// Filename: CreateUserCommandHandler.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class CreateUserCommandHandler : AsyncCommandHandler<CreateUserCommand>
    {
        private readonly CustomerService _service;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository, CustomerService service)
        {
            _userRepository = userRepository;
            _service = service;
        }

        protected override async Task Handle([NotNull] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var options = new CustomerCreateOptions
            {
                Email = command.Email,
                Metadata = new Dictionary<string, string>
                {
                    {
                        nameof(command.UserId), command.UserId.ToString()
                    }
                }
            };

            var customer = await _service.CreateAsync(options, cancellationToken: cancellationToken);

            var user = new User(command.UserId, CustomerId.Parse(customer.Id));

            _userRepository.Create(user);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
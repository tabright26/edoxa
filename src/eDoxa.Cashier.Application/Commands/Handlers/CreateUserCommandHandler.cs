﻿// Filename: CreateUserCommandHandler.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    public sealed class CreateUserCommandHandler : AsyncCommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly CustomerService _service;

        public CreateUserCommandHandler(IUserRepository userRepository, CustomerService service)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        protected override async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
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

            var user = User.Create(command.UserId, CustomerId.Parse(customer.Id));

            _userRepository.Create(user);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
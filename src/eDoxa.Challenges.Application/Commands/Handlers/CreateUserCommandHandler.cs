﻿// Filename: CreateUserCommandHandler.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

namespace eDoxa.Challenges.Application.Commands.Handlers
{
    public class CreateUserCommandHandler : AsyncCommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        protected override async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User(command.UserId);

            _userRepository.Create(user);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
// Filename: CreateUserCommandHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;
using JetBrains.Annotations;

namespace eDoxa.Notifications.Application.Commands.Handlers
{
    public class CreateUserCommandHandler : AsyncCommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task Handle([NotNull] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = User.Create(command.UserId);

            _userRepository.Create(user);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
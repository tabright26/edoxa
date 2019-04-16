// Filename: NotifyUserCommandHandler.cs
// Date Created: 2019-03-04
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

using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;
using JetBrains.Annotations;

namespace eDoxa.Notifications.Application.Commands.Handlers
{
    public class NotifyUserCommandHandler : AsyncCommandHandler<NotifyUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public NotifyUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        protected override async Task Handle([NotNull] NotifyUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindAsync(command.UserId);

            user.Notify(command.Title, command.RedirectUrl, command.Message);

            await _userRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
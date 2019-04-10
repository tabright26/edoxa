// Filename: ReadNotificationCommandHandler.cs
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

namespace eDoxa.Notifications.Application.Commands.Handlers
{
    public class ReadNotificationCommandHandler : AsyncCommandHandler<ReadNotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository;

        public ReadNotificationCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
        }

        protected override async Task Handle(ReadNotificationCommand command, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.FindAsync(command.NotificationId);

            notification.Read();

            await _notificationRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
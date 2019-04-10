// Filename: DeleteNotificationCommand.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Seedwork.Application.Commands;

namespace eDoxa.Notifications.Application.Commands
{
    [DataContract]
    public class DeleteNotificationCommand : Command
    {
        public DeleteNotificationCommand(NotificationId notificationId)
        {
            NotificationId = notificationId;
        }

        [IgnoreDataMember]
        public NotificationId NotificationId { get; private set; }
    }
}
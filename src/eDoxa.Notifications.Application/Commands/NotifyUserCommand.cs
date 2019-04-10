// Filename: NotifyUserCommand.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Notifications.Domain;
using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Seedwork.Application.Commands;

namespace eDoxa.Notifications.Application.Commands
{
    [DataContract]
    public class NotifyUserCommand : Command
    {
        public NotifyUserCommand(UserId recipientId, string name, INotificationMetadata metadata = null, string redirectUrl = null)
        {
            UserId = recipientId;
            Name = name;
            RedirectUrl = redirectUrl;
            Metadata = metadata;
        }

        [DataMember]
        public UserId UserId { get; }

        [DataMember]
        public string Name { get; }

        [DataMember(IsRequired = false)]
        public INotificationMetadata Metadata { get; }

        [DataMember(IsRequired = false)]
        public string RedirectUrl { get; }
    }
}
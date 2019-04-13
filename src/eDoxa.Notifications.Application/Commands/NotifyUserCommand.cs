// Filename: NotifyUserCommand.cs
// Date Created: 2019-04-13
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
    public class NotifyUserCommand : Command
    {
        public NotifyUserCommand(UserId userId, string title, string message, string redirectUrl = null)
        {
            UserId = userId;
            Title = title;
            Message = message;
            RedirectUrl = redirectUrl;
        }

        [DataMember]
        public UserId UserId { get; private set; }

        [DataMember]
        public string Title { get; private set; }

        [DataMember(IsRequired = false)]
        public string Message { get; private set; }

        [DataMember(IsRequired = false)]
        public string RedirectUrl { get; private set; }
    }
}
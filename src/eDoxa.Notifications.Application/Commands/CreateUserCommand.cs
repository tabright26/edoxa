﻿// Filename: CreateUserCommand.cs
// Date Created: 2019-03-04
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
    public class CreateUserCommand : Command
    {
        public CreateUserCommand(UserId userId)
        {
            UserId = userId;
        }

        [DataMember]
        public UserId UserId { get; private set; }
    }
}
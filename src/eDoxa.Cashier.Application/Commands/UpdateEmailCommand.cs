// Filename: UpdateEmailCommand.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Application.Commands;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public class UpdateEmailCommand : Command
    {
        public UpdateEmailCommand(UserId userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        [DataMember]
        public UserId UserId { get; private set; }

        [DataMember]
        public string Email { get; }
    }
}
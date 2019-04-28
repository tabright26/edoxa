// Filename: UpdateEmailCommand.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Commands.Abstractions;

namespace eDoxa.Cashier.Application.Commands
{
    [DataContract]
    public sealed class UpdateEmailCommand : Command
    {
        public UpdateEmailCommand(UserId userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        [DataMember(Name = "userId")] public UserId UserId { get; private set; }

        [DataMember(Name = "email")] public string Email { get; }
    }
}
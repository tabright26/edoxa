// Filename: UpdatePhoneCommand.cs
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
    public sealed class UpdatePhoneCommand : Command
    {
        public UpdatePhoneCommand(UserId userId, string phone)
        {
            UserId = userId;
            Phone = phone;
        }

        [DataMember(Name = "userId")] public UserId UserId { get; private set; }

        [DataMember(Name = "phone")] public string Phone { get; private set; }
    }
}
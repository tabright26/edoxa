// Filename: CreateUserCommand.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Commands.Abstractions;

namespace eDoxa.Challenges.Application.Commands
{
    [DataContract]
    public sealed class CreateUserCommand : Command
    {
        public CreateUserCommand(UserId userId)
        {
            UserId = userId;
        }

        [DataMember(Name = "userId")] public UserId UserId { get; private set; }
    }
}
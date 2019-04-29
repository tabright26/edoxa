// Filename: RegisterChallengeParticipantCommand.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Commands.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Application.Commands
{
    [DataContract]
    public sealed class RegisterChallengeParticipantCommand : Command<IActionResult>
    {
        public RegisterChallengeParticipantCommand(UserId userId)
        {
            UserId = userId;
        }

        [IgnoreDataMember] public LinkedAccount LinkedAccount { get; set; }

        [IgnoreDataMember] public ChallengeId ChallengeId { get; set; }

        [DataMember(Name = "userId")] public UserId UserId { get; private set; }
    }
}
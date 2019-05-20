// Filename: RegisterParticipantCommand.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Commands.Abstractions;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Application.Commands
{
    [DataContract]
    public sealed class RegisterParticipantCommand : Command<IActionResult>
    {
        public RegisterParticipantCommand(ChallengeId challengeId)
        {
            ChallengeId = challengeId;
        }

        [IgnoreDataMember] public ChallengeId ChallengeId { get; private set; }
    }
}
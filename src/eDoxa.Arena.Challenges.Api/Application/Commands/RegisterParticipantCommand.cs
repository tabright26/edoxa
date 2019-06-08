// Filename: RegisterParticipantCommand.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Commands.Abstractions;

namespace eDoxa.Arena.Challenges.Api.Application.Commands
{
    [DataContract]
    public sealed class RegisterParticipantCommand : Command<ParticipantViewModel>
    {
        public RegisterParticipantCommand(ChallengeId challengeId)
        {
            ChallengeId = challengeId;
        }

        [IgnoreDataMember] public ChallengeId ChallengeId { get; private set; }
    }
}

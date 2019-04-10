// Filename: RegisterChallengeParticipantCommand.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Application.Commands;

namespace eDoxa.Challenges.Application.Commands
{
    [DataContract]
    public class RegisterChallengeParticipantCommand : Command
    {
        public RegisterChallengeParticipantCommand(ChallengeId challengeId, UserId userId)
        {
            ChallengeId = challengeId;
            UserId = userId;
        }

        [IgnoreDataMember]
        public LinkedAccount LinkedAccount { get; set; }

        [DataMember]
        public ChallengeId ChallengeId { get; private set; }

        [DataMember]
        public UserId UserId { get; private set; }
    }
}
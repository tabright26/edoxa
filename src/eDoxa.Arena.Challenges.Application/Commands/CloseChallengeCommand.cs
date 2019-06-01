// Filename: CompleteChallengeCommand.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Seedwork.Application.Commands.Abstractions;

namespace eDoxa.Arena.Challenges.Application.Commands
{
    [DataContract]
    public sealed class CloseChallengeCommand : Command
    {
        public CloseChallengeCommand(ChallengeId challengeId)
        {
            ChallengeId = challengeId;
        }

        [IgnoreDataMember]
        public ChallengeId ChallengeId { get; private set; }
    }
}

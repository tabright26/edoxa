// Filename: IParticipantQuery.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.ViewModels;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Abstractions.Queries
{
    public interface IParticipantQuery
    {
        Task<IReadOnlyCollection<ParticipantViewModel>> FindChallengeParticipantsAsync(ChallengeId challengeId);

        [ItemCanBeNull]
        Task<ParticipantViewModel> FindParticipantAsync(ParticipantId participantId);
    }
}

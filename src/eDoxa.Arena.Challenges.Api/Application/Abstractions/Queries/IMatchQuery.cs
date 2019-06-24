// Filename: IMatchQuery.cs
// Date Created: 2019-06-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Abstractions.Queries
{
    public interface IMatchQuery
    {
        Task<IReadOnlyCollection<MatchViewModel>> FindParticipantMatchesAsync(ParticipantId participantId);

        [ItemCanBeNull]
        Task<MatchViewModel> FindMatchAsync(MatchId matchId);
    }
}

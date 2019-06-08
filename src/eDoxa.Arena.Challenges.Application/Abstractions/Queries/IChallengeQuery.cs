// Filename: IChallengeQuery.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Abstractions.Queries
{
    public interface IChallengeQuery
    {
        Task<IReadOnlyCollection<ChallengeViewModel>> GetChallengesAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);

        [ItemCanBeNull]
        Task<ChallengeViewModel> GetChallengeAsync(ChallengeId challengeId);

        Task<IReadOnlyCollection<ChallengeViewModel>> FindUserChallengeHistoryAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);
    }
}

// Filename: IChallengeQuery.cs
// Date Created: 2019-06-25
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

namespace eDoxa.Arena.Challenges.Domain.Queries
{
    public interface IChallengeQuery
    {
        Task<IReadOnlyCollection<ChallengeViewModel>> FindUserChallengeHistoryAsync(ChallengeGame game = null, ChallengeState state = null);

        Task<IReadOnlyCollection<ChallengeViewModel>> FindChallengesAsync(ChallengeGame game = null, ChallengeState state = null);

        [ItemCanBeNull]
        Task<ChallengeViewModel> FindChallengeAsync(ChallengeId challengeId);
    }
}

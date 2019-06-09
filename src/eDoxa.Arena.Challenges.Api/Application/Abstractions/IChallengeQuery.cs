// Filename: IChallengeQuery.cs
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

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Abstractions
{
    public interface IChallengeQuery
    {
        Task<IReadOnlyCollection<ChallengeViewModel>> GetChallengesAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);

        [ItemCanBeNull]
        Task<ChallengeViewModel> GetChallengeAsync(ChallengeId challengeId);

        Task<IReadOnlyCollection<ChallengeViewModel>> FindUserChallengeHistoryAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);
    }
}

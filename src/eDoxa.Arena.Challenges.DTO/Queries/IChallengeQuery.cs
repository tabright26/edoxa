﻿// Filename: IChallengeQuery.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.DTO.Queries
{
    public interface IChallengeQuery
    {
        Task<IReadOnlyCollection<ChallengeDTO>> GetChallengesAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);

        [ItemCanBeNull]
        Task<ChallengeDTO> GetChallengeAsync(ChallengeId challengeId);

        Task<IReadOnlyCollection<ChallengeDTO>> FindUserChallengeHistoryAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null);
    }
}

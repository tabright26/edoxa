// Filename: IGamesApiRefitClient.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Api.Areas.Challenges.RefitClients
{
    public interface IGamesApiRefitClient
    {
        Task<IEnumerable<Match>> GetMatchesAsync(PlayerId playerId, DateTime? startedAt, DateTime? closedAt);
    }
}

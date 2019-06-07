// Filename: IMatchStatsFactory.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Abstractions
{
    public interface IMatchStatsFactory
    {
        Task<IMatchStatsAdapter> CreateAdapter(Game game, ExternalAccount externalAccount, MatchReference matchReference);
    }
}

// Filename: IMatchExternalIdsFactory.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Abstractions
{
    public interface IMatchReferencesFactory
    {
        Task<IMatchReferencesAdapter> CreateAdapterAsync(Game game, ExternalAccount externalAccount, ChallengeTimeline timeline);
    }
}

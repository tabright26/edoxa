// Filename: IFakeChallengeService.cs
// Date Created: 2019-05-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Domain.Services
{
    public interface IFakeChallengeService
    {
        Task<Challenge> CreateChallenge(
            IFakeChallengeBuilder builder,
            bool registerParticipants,
            bool snapshotParticipantMatches,
            CancellationToken cancellationToken = default
        );
    }
}

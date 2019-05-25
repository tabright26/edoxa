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
using eDoxa.Arena.Domain;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Challenges.Domain.Services
{
    public interface IFakeChallengeService
    {
        Task<Challenge> CreateChallenge(
            ChallengeName name,
            Game game,
            BestOf bestOf,
            PayoutEntries payoutEntries,
            EntryFee entryFee,
            bool equivalentCurrency = true,
            bool registerParticipants = false,
            bool snapshotParticipantMatches = false,
            CancellationToken cancellationToken = default
        );
    }
}

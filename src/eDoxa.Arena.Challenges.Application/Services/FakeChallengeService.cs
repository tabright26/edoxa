// Filename: FakeChallengeService.cs
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
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;

namespace eDoxa.Arena.Challenges.Application.Services
{
    public sealed class FakeChallengeService : IFakeChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public FakeChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<Challenge> CreateChallenge(
            IFakeChallengeBuilder builder,
            bool registerParticipants,
            bool snapshotParticipantMatches,
            CancellationToken cancellationToken = default
        )
        {
            if (registerParticipants)
            {
                builder.RegisterParticipants();
            }

            if (snapshotParticipantMatches)
            {
                builder.SnapshotParticipantMatches();
            }

            var challenge = builder.Build();

            _challengeRepository.Create(challenge);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return challenge;
        }
    }
}

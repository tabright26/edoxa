// Filename: CreateTokenChallengeCommandHandler.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
{
    internal sealed class CreateTokenChallengeCommandHandler : ICommandHandler<CreateTokenChallengeCommand, Either<ValidationError, ChallengeDTO>>
    {
        private readonly IFakeChallengeService _challengeService;
        private readonly IMapper _mapper;

        public CreateTokenChallengeCommandHandler(IFakeChallengeService challengeService, IMapper mapper)
        {
            _challengeService = challengeService;
            _mapper = mapper;
        }

        [ItemCanBeNull]
        public async Task<Either<ValidationError, ChallengeDTO>> Handle([NotNull] CreateTokenChallengeCommand command, CancellationToken cancellationToken)
        {
            var challenge = await _challengeService.CreateChallenge(
                new ChallengeName(command.Name),
                command.Game,
                command.BestOf,
                command.PayoutEntries,
                command.EntryFee,
                command.EquivalentCurrency,
                command.RegisterParticipants,
                command.SnapshotParticipantMatches,
                cancellationToken
            );

            return _mapper.Map<ChallengeDTO>(challenge);
        }
    }
}

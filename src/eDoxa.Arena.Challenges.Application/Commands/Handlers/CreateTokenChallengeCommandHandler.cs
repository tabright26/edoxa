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
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
{
    internal sealed class CreateTokenChallengeCommandHandler : ICommandHandler<CreateTokenChallengeCommand, Either<ValidationError, ChallengeDTO>>
    {
        private readonly IChallengeService _challengeService;
        private readonly IMapper _mapper;

        public CreateTokenChallengeCommandHandler(IChallengeService challengeService, IMapper mapper)
        {
            _challengeService = challengeService;
            _mapper = mapper;
        }

        [ItemCanBeNull]
        public async Task<Either<ValidationError, ChallengeDTO>> Handle([NotNull] CreateTokenChallengeCommand command, CancellationToken cancellationToken)
        {
            var either = await _challengeService.CreateChallengeAsync(
                new ChallengeName(command.Name),
                command.Game,
                command.BestOf,
                command.EntryFee,
                command.PayoutEntries,
                command.EquivalentCurrency,
                command.IsFakeChallenge,
                cancellationToken
            );

            return either.Match<Either<ValidationError, ChallengeDTO>>(error => error, challenge => _mapper.Map<ChallengeDTO>(challenge));
        }
    }
}

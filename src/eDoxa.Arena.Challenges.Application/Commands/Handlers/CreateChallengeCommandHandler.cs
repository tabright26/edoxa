// Filename: CreateChallengeCommandHandler.cs
// Date Created: 2019-05-29
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

using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Application.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
{
    public sealed class CreateChallengeCommandHandler : ICommandHandler<CreateChallengeCommand, ChallengeViewModel>
    {
        private readonly IChallengeService _challengeService;
        private readonly IMapper _mapper;

        public CreateChallengeCommandHandler(IChallengeService challengeService, IMapper mapper)
        {
            _challengeService = challengeService;
            _mapper = mapper;
        }

        [ItemCanBeNull]
        public async Task<ChallengeViewModel> Handle([NotNull] CreateChallengeCommand command, CancellationToken cancellationToken)
        {
            var challenge = await _challengeService.CreateChallengeAsync(
                command.Name,
                command.Game,
                command.Duration,
                command.BestOf,
                command.PayoutEntries,
                _mapper.Map<EntryFee>(command.EntryFee),
                _mapper.Map<TestMode>(command.TestMode),
                cancellationToken
            );

            return _mapper.Map<ChallengeViewModel>(challenge);
        }
    }
}

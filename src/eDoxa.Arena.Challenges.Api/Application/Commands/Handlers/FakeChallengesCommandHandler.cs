// Filename: FakeChallengesCommandHandler.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Abstractions.Services;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Commands.Handlers
{
    public sealed class FakeChallengesCommandHandler : ICommandHandler<FakeChallengesCommand, IEnumerable<ChallengeViewModel>>
    {
        private readonly IChallengeService _challengeService;
        private readonly IMapper _mapper;

        public FakeChallengesCommandHandler(IChallengeService challengeService, IMapper mapper)
        {
            _challengeService = challengeService;
            _mapper = mapper;
        }

        [ItemCanBeNull]
        public async Task<IEnumerable<ChallengeViewModel>> Handle([NotNull] FakeChallengesCommand command, CancellationToken cancellationToken)
        {
            var challenges = await _challengeService.FakeChallengesAsync(
                command.Count,
                command.Seed,
                command.Game,
                command.State,
                command.EntryFeeCurrency,
                cancellationToken
            );

            return _mapper.Map<IEnumerable<ChallengeViewModel>>(challenges);
        }
    }
}

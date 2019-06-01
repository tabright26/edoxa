// Filename: RegisterParticipantCommandHandler.cs
// Date Created: 2019-05-20
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

using eDoxa.Arena.Application.Extensions;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Application.Commands.Abstractions.Handlers;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
{
    public sealed class RegisterParticipantCommandHandler : ICommandHandler<RegisterParticipantCommand, ParticipantDTO>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IChallengeService _challengeService;
        private readonly IMapper _mapper;

        public RegisterParticipantCommandHandler(IHttpContextAccessor httpContextAccessor, IChallengeService challengeService, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _challengeService = challengeService;
            _mapper = mapper;
        }
        [ItemNotNull]
        public async Task<ParticipantDTO> Handle([NotNull] RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var participant = await _challengeService.RegisterParticipantAsync(
                command.ChallengeId,
                userId,
                _httpContextAccessor.FuncExternalAccount(),
                cancellationToken
            );

            return _mapper.Map<ParticipantDTO>(participant);
        }
    }
}

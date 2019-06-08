// Filename: RegisterParticipantCommandHandler.cs
// Date Created: 2019-06-07
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

using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.Abstractions.Services;
using eDoxa.Seedwork.Application.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Security.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Api.Application.Commands.Handlers
{
    public sealed class RegisterParticipantCommandHandler : ICommandHandler<RegisterParticipantCommand, ParticipantViewModel>
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
        public async Task<ParticipantViewModel> Handle([NotNull] RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var participant = await _challengeService.RegisterParticipantAsync(
                command.ChallengeId,
                userId,
                _httpContextAccessor.FuncExternalAccount(),
                cancellationToken
            );

            return _mapper.Map<ParticipantViewModel>(participant);
        }
    }
}

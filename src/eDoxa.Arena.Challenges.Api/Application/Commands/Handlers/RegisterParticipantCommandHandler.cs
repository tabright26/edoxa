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

using eDoxa.Arena.Challenges.Api.Application.Abstractions.Services;
using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Seedwork.Security.Extensions;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Api.Application.Commands.Handlers
{
    public sealed class RegisterParticipantCommandHandler : AsyncCommandHandler<RegisterParticipantCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IChallengeService _challengeService;

        public RegisterParticipantCommandHandler(IHttpContextAccessor httpContextAccessor, IChallengeService challengeService)
        {
            _httpContextAccessor = httpContextAccessor;
            _challengeService = challengeService;
        }

        protected override async Task Handle(RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            await _challengeService.RegisterParticipantAsync(command.ChallengeId, userId, _httpContextAccessor.FuncUserGameReference(), cancellationToken);
        }
    }
}

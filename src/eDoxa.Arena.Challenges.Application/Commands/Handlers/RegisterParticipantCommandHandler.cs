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

using eDoxa.Arena.Application.Extensions;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Functional;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Domain.Validations;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
{
    internal sealed class RegisterParticipantCommandHandler : ICommandHandler<RegisterParticipantCommand, Either<ValidationError, string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IChallengeService _challengeService;

        public RegisterParticipantCommandHandler(IHttpContextAccessor httpContextAccessor, IChallengeService challengeService)
        {
            _httpContextAccessor = httpContextAccessor;
            _challengeService = challengeService;
        }

        [ItemNotNull]
        public async Task<Either<ValidationError, string>> Handle([NotNull] RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var either = await _challengeService.RegisterParticipantAsync(
                command.ChallengeId,
                userId,
                _httpContextAccessor.FuncExternalAccount(),
                cancellationToken
            );

            return either.Match<Either<ValidationError, string>>(x => x, x => "The participant has registered successfully.");
        }
    }
}

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

using FluentValidation.Results;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
{
    public sealed class RegisterParticipantCommandHandler : ICommandHandler<RegisterParticipantCommand, Either<ValidationResult, string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IChallengeService _challengeService;

        public RegisterParticipantCommandHandler(IHttpContextAccessor httpContextAccessor, IChallengeService challengeService)
        {
            _httpContextAccessor = httpContextAccessor;
            _challengeService = challengeService;
        }

        [ItemNotNull]
        public async Task<Either<ValidationResult, string>> Handle([NotNull] RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

            var either = await _challengeService.RegisterParticipantAsync(
                command.ChallengeId,
                userId,
                _httpContextAccessor.FuncExternalAccount(),
                cancellationToken
            );

            return either.Match<Either<ValidationResult, string>>(x => x, x => "The participant has registered successfully.");
        }
    }
}

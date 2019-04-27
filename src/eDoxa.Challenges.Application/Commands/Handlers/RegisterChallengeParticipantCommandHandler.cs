// Filename: RegisterChallengeParticipantCommandHandler.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Application.Commands.Handlers
{
    internal sealed class RegisterChallengeParticipantCommandHandler : ICommandHandler<RegisterChallengeParticipantCommand, IActionResult>
    {
        private readonly IChallengeRepository _challengeRepository;

        public RegisterChallengeParticipantCommandHandler(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] RegisterChallengeParticipantCommand command, CancellationToken cancellationToken)
        {
            var challenge = await _challengeRepository.FindChallengeAsync(command.ChallengeId);

            challenge.RegisterParticipant(command.UserId, command.LinkedAccount);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return new OkResult();
        }
    }
}
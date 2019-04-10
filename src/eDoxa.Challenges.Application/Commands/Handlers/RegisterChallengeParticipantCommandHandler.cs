// Filename: RegisterChallengeParticipantCommandHandler.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Application.Commands.Handlers;

namespace eDoxa.Challenges.Application.Commands.Handlers
{
    public class RegisterChallengeParticipantCommandHandler : AsyncCommandHandler<RegisterChallengeParticipantCommand>
    {
        private readonly IChallengeRepository _challengeRepository;

        public RegisterChallengeParticipantCommandHandler(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository ?? throw new ArgumentNullException(nameof(challengeRepository));
        }

        protected override async Task Handle(RegisterChallengeParticipantCommand command, CancellationToken cancellationToken)
        {
            var challenge = await _challengeRepository.FindChallengeAsync(command.ChallengeId);

            challenge.RegisterParticipant(command.UserId, command.LinkedAccount);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
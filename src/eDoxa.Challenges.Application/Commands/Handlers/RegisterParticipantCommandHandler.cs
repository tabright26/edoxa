// Filename: RegisterParticipantCommandHandler.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Specifications;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate.Specifications;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Abstractions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Application.Commands.Handlers
{
    internal sealed class RegisterParticipantCommandHandler : ICommandHandler<RegisterParticipantCommand, IActionResult>
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IUserProfile _userProfile;

        public RegisterParticipantCommandHandler(IUserProfile userProfile, IChallengeRepository challengeRepository)
        {
            _userProfile = userProfile;
            _challengeRepository = challengeRepository;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Parse(_userProfile.Subject);

            var challenge = await _challengeRepository.FindChallengeAsync(command.ChallengeId);

            if (challenge == null)
            {
                return new NotFoundObjectResult("Challenge not found.");
            }

            if (new ParticipantAlreadyRegisteredSpecification(userId).IsSatisfiedBy(challenge))
            {
                return new BadRequestObjectResult("The participant is already registered for the challenge.");
            }

            if (new ChallengeIsFullSpecification().IsSatisfiedBy(challenge))
            {
                return new BadRequestObjectResult("Registration of participants is complete.");
            }

            if (new ChallengeOpenedSpecification().Not().IsSatisfiedBy(challenge))
            {
                return new BadRequestObjectResult("The state of the challenge is not open for registration.");
            }

            challenge.RegisterParticipant(userId, command.LinkedAccount);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return new OkObjectResult("The participant has registered successfully.");
        }
    }
}
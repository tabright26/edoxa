// Filename: RegisterChallengeParticipantCommandHandler.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Specifications;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate.Specifications;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Challenges.Application.Commands.Handlers
{
    internal sealed class RegisterChallengeParticipantCommandHandler : ICommandHandler<RegisterChallengeParticipantCommand, IActionResult>
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IChallengeRepository _challengeRepository;

        public RegisterChallengeParticipantCommandHandler(IUserInfoService userInfoService, IChallengeRepository challengeRepository)
        {
            _userInfoService = userInfoService;
            _challengeRepository = challengeRepository;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] RegisterChallengeParticipantCommand command, CancellationToken cancellationToken)
        {
            var userId = _userInfoService.Subject.Select(UserId.FromGuid).SingleOrDefault();

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
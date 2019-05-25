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

using eDoxa.Arena.Application.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Specifications;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate.Specifications;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security.Extensions;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Arena.Challenges.Application.Commands.Handlers
{
    internal sealed class RegisterParticipantCommandHandler : ICommandHandler<RegisterParticipantCommand, IActionResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IChallengeRepository _challengeRepository;

        public RegisterParticipantCommandHandler(IHttpContextAccessor httpContextAccessor, IChallengeRepository challengeRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _challengeRepository = challengeRepository;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] RegisterParticipantCommand command, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();

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

            //if (new ChallengeOpenedSpecification().Not().IsSatisfiedBy(challenge))
            //{
            //    return new BadRequestObjectResult("The state of the challenge is not open for registration.");
            //}

            var externalAccount = _httpContextAccessor.GetExternalAccount(challenge.Game);

            if (externalAccount == null)
            {
                // TODO: Change this redirect result to a specific game external login process (Location).
                return new BadRequestObjectResult($"This user does not have an account for the game: {challenge.Game}.");
            }

            challenge.RegisterParticipant(userId, externalAccount);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return new OkObjectResult("The participant has registered successfully.");
        }
    }
}
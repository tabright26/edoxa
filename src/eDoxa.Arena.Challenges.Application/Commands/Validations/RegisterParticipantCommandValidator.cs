// Filename: RegisterParticipantCommandValidator.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Application.Extensions;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Validators;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Application.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Extensions;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Application.Commands.Validations
{
    public sealed class RegisterParticipantCommandValidator : CommandValidator<RegisterParticipantCommand>
    {
        public RegisterParticipantCommandValidator(IHttpContextAccessor httpContextAccessor, IChallengeRepository challengeRepository)
        {
            this.EntityId(command => command.ChallengeId)
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(command => command)
                            .CustomAsync(
                                async (command, context, cancellationToken) =>
                                {
                                    var challenge = await challengeRepository.FindChallengeAsNoTrackingAsync(command.ChallengeId);

                                    if (challenge == null)
                                    {
                                        context.AddFailure(
                                            new ValidationFailure(null, "Challenge not found.")
                                            {
                                                ErrorCode = "NotFound"
                                            }
                                        );
                                    }
                                    else
                                    {
                                        var userId = httpContextAccessor.GetUserId();

                                        var externalAccount = httpContextAccessor.FuncExternalAccount();

                                        if (externalAccount(challenge.Game) == null)
                                        {
                                            context.AddFailure("This user does not provide an external account for the challenge-specific game.");
                                        }
                                        else
                                        {
                                            new RegisterParticipantValidator(userId).Validate(challenge).Errors.ForEach(context.AddFailure);
                                        }
                                    }
                                }
                            );
                    }
                );
        }
    }
}

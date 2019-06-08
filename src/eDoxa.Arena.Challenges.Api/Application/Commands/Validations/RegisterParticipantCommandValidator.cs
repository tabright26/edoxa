// Filename: RegisterParticipantCommandValidator.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.Validators;
using eDoxa.Seedwork.Application.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Security.Extensions;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Api.Application.Commands.Validations
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

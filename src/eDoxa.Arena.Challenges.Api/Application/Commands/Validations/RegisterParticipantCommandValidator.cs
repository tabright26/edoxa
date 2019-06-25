// Filename: RegisterParticipantCommandValidator.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Commands.Abstractions.Validations;
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Api.Application.Commands.Validations
{
    public sealed class RegisterParticipantCommandValidator : CommandValidator<RegisterParticipantCommand>
    {
        public RegisterParticipantCommandValidator(IHttpContextAccessor httpContextAccessor, IChallengeQuery challengeQuery)
        {
            this.EntityId(command => command.ChallengeId)
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(command => command)
                            .CustomAsync(
                                async (command, context, cancellationToken) =>
                                {
                                    var challenge = await challengeQuery.FindChallengeAsync(command.ChallengeId);

                                    if (challenge == null)
                                    {
                                        context.AddFailure(
                                            new ValidationFailure(null, "Challenge not found.")
                                            {
                                                ErrorCode = "NotFound"
                                            }
                                        );
                                    }
                                }
                            );
                    }
                );
        }
    }
}

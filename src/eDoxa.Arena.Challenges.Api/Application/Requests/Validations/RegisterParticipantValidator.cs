// Filename: RegisterParticipantValidator.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Arena.Challenges.Api.Application.Requests.Validations
{
    public sealed class RegisterParticipantValidator : AbstractValidator<RegisterParticipantRequest>
    {
        public RegisterParticipantValidator(IHttpContextAccessor httpContextAccessor, IChallengeQuery challengeQuery)
        {
            this.EntityId(request => request.ChallengeId)
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(request => request)
                            .CustomAsync(
                                async (request, context, cancellationToken) =>
                                {
                                    var challenge = await challengeQuery.FindChallengeAsync(request.ChallengeId);

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

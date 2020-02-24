// Filename: RegisterChallengeParticipantRequestValidator.cs
// Date Created: 2020-02-23
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Challenges.Web.Aggregator.Requests;

using FluentValidation;

namespace eDoxa.Challenges.Web.Aggregator.Validators
{
    public class RegisterChallengeParticipantRequestValidator : AbstractValidator<RegisterChallengeParticipantRequest>
    {
        public RegisterChallengeParticipantRequestValidator()
        {
            this.RuleFor(request => request.ChallengeId).NotNull().NotEmpty();
        }
    }
}

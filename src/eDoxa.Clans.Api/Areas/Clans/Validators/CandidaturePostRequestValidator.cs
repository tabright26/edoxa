// Filename: CandidaturePostRequestValidator.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Clans.Api.Areas.Clans.ErrorDescribers;
using eDoxa.Clans.Requests;

using FluentValidation;

namespace eDoxa.Clans.Api.Areas.Clans.Validators
{
    public sealed class CandidaturePostRequestValidator : AbstractValidator<CandidaturePostRequest>
    {
        public CandidaturePostRequestValidator()
        {
            this.RuleFor(request => request.UserId)
                .NotNull()
                .WithMessage(CandidatureErrorDescriber.UserIdRequired())
                .NotEmpty()
                .WithMessage(CandidatureErrorDescriber.UserIdRequired());

            this.RuleFor(request => request.ClanId)
                .NotNull()
                .WithMessage(CandidatureErrorDescriber.UserIdRequired())
                .NotEmpty()
                .WithMessage(CandidatureErrorDescriber.ClanIdRequired());
        }
    }
}

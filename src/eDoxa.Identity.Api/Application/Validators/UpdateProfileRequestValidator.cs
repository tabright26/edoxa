// Filename: UpdateProfileRequestValidator.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.ErrorDescribers;

using FluentValidation;

namespace eDoxa.Identity.Api.Application.Validators
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator()
        {
            this.RuleFor(request => request.FirstName)
                .NotNull()
                .WithMessage(ProfileErrorDescriber.FirstNameRequired())
                .NotEmpty()
                .WithMessage(ProfileErrorDescriber.FirstNameRequired())
                .Length(2, 16)
                .WithMessage(ProfileErrorDescriber.FirstNameLength())
                .Matches(new Regex("^[A-Za-z]((-)[a-zA-Z]|[a-z]){1,15}$"))
                .WithMessage(ProfileErrorDescriber.FirstNameInvalid());
        }
    }
}

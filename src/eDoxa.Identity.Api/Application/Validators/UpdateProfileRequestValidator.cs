// Filename: UpdateProfileRequestValidator.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage(ProfileErrorDescriber.FirstNameInvalid())
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage(ProfileErrorDescriber.FirstNameUppercase());
        }
    }
}

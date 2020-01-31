// Filename: CreateProfileRequestValidator.cs
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
    public class CreateProfileRequestValidator : AbstractValidator<CreateProfileRequest>
    {
        public CreateProfileRequestValidator()
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

            this.RuleFor(request => request.LastName)
                .NotNull()
                .WithMessage(ProfileErrorDescriber.LastNameRequired())
                .NotEmpty()
                .WithMessage(ProfileErrorDescriber.LastNameRequired())
                .Length(2, 16)
                .WithMessage(ProfileErrorDescriber.LastNameLength())
                .Matches(new Regex("^[A-Za-z]((-)[a-zA-Z]|[a-z]){1,15}$"))
                .WithMessage(ProfileErrorDescriber.LastNameInvalid());

            this.RuleFor(request => request.Gender)
                .NotNull()
                .WithMessage(ProfileErrorDescriber.GenderRequired())
                .NotEmpty()
                .WithMessage(ProfileErrorDescriber.GenderRequired());
        }
    }
}

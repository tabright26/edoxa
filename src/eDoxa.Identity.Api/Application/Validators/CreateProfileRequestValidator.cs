// Filename: CreateProfileRequestValidator.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
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
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage(ProfileErrorDescriber.FirstNameInvalid())
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage(ProfileErrorDescriber.FirstNameUppercase());

            this.RuleFor(request => request.LastName)
                .NotNull()
                .WithMessage(ProfileErrorDescriber.LastNameRequired())
                .NotEmpty()
                .WithMessage(ProfileErrorDescriber.LastNameRequired())
                .Length(2, 16)
                .WithMessage(ProfileErrorDescriber.LastNameLength())
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage(ProfileErrorDescriber.LastNameInvalid())
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage(ProfileErrorDescriber.LastNameUppercase());

            this.RuleFor(request => request.Gender)
                .NotNull()
                .WithMessage(ProfileErrorDescriber.GenderRequired())
                .NotEmpty()
                .WithMessage(ProfileErrorDescriber.GenderRequired());

            ////https://stackoverflow.com/questions/7777985/validate-datetime-with-fluentvalidator
            //this.RuleFor(request => request.Dob)
            //    .Must(request => BeAValidDate(new DateTime(request.Year, request.Month, request.Day)))
            //    .WithMessage(InformationsErrorDescriber.BirthDateInvalid());
        }

        private static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}

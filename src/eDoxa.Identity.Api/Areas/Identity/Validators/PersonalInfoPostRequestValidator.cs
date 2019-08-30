// Filename: PersonalInfoPostRequestValidator.cs
// Date Created: 2019-08-22
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Text.RegularExpressions;

using eDoxa.Identity.Api.Areas.Identity.ErrorDescribers;
using eDoxa.Identity.Api.Areas.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public class PersonalInfoPostRequestValidator : AbstractValidator<PersonalInfoPostRequest>
    {
        public PersonalInfoPostRequestValidator()
        {
            this.RuleFor(request => request.FirstName)
                .NotNull()
                .WithMessage(PersonalInfoErrorDescriber.FirstNameRequired())
                .NotEmpty()
                .WithMessage(PersonalInfoErrorDescriber.FirstNameRequired())
                .Length(2, 16)
                .WithMessage(PersonalInfoErrorDescriber.FirstNameLength())
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage(PersonalInfoErrorDescriber.FirstNameInvalid())
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage(PersonalInfoErrorDescriber.FirstNameUppercase());

            this.RuleFor(request => request.LastName)
                .NotNull()
                .WithMessage(PersonalInfoErrorDescriber.LastNameRequired())
                .NotEmpty()
                .WithMessage(PersonalInfoErrorDescriber.LastNameRequired())
                .Length(2, 16)
                .WithMessage(PersonalInfoErrorDescriber.LastNameLength())
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage(PersonalInfoErrorDescriber.LastNameInvalid())
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage(PersonalInfoErrorDescriber.LastNameUppercase());

            this.RuleFor(request => request.Gender)
                .NotNull()
                .WithMessage(PersonalInfoErrorDescriber.GenderRequired())
                .NotEmpty()
                .WithMessage(PersonalInfoErrorDescriber.GenderRequired());

            //https://stackoverflow.com/questions/7777985/validate-datetime-with-fluentvalidator
            this.RuleFor(request => request.BirthDate)
                .NotNull()
                .WithMessage(PersonalInfoErrorDescriber.BirthDateRequired())
                .NotEmpty()
                .WithMessage(PersonalInfoErrorDescriber.BirthDateRequired())
                .Must(BeAValidDate)
                .WithMessage(PersonalInfoErrorDescriber.BirthDateInvalid());
        }

        private static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}

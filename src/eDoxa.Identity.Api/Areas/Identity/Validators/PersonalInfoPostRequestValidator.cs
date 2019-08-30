// Filename: PersonalInfoPostRequestValidator.cs
// Date Created: 2019-08-22
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Text.RegularExpressions;

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
                .WithMessage("First name is required")
                .NotEmpty()
                .WithMessage("First name is required")
                .Length(2, 16)
                .WithMessage("First name must be between 2 and 16 characters long")
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage("First name invalid. Only letters and hyphens allowed")
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage("First name invalid. Every part must start with an uppercase");

            this.RuleFor(request => request.LastName)
                .NotNull()
                .WithMessage("Last name is required")
                .NotEmpty()
                .WithMessage("Last name is required")
                .Length(2, 16)
                .WithMessage("Last name must be between 2 and 16 characters long")
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage("Last name invalid. Only letters and hyphens allowed")
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage("Last name invalid. Every part must start with an uppercase");

            this.RuleFor(request => request.Gender).NotNull().WithMessage("Gender is required").NotEmpty().WithMessage("Gender is required");

            //https://stackoverflow.com/questions/7777985/validate-datetime-with-fluentvalidator
            this.RuleFor(request => request.BirthDate)
                .NotNull()
                .WithMessage("Birth date is required")
                .NotEmpty()
                .WithMessage("Birth date  is required")
                .Must(BeAValidDate)
                .WithMessage("Birth date invalid");
        }

        private static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}

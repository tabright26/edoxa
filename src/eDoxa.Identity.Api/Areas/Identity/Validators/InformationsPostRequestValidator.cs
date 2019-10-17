// Filename: InformationsPostRequestValidator.cs
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
    public class InformationsPostRequestValidator : AbstractValidator<InformationsPostRequest>
    {
        public InformationsPostRequestValidator()
        {
            this.RuleFor(request => request.FirstName)
                .NotNull()
                .WithMessage(InformationsErrorDescriber.FirstNameRequired())
                .NotEmpty()
                .WithMessage(InformationsErrorDescriber.FirstNameRequired())
                .Length(2, 16)
                .WithMessage(InformationsErrorDescriber.FirstNameLength())
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage(InformationsErrorDescriber.FirstNameInvalid())
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage(InformationsErrorDescriber.FirstNameUppercase());

            this.RuleFor(request => request.LastName)
                .NotNull()
                .WithMessage(InformationsErrorDescriber.LastNameRequired())
                .NotEmpty()
                .WithMessage(InformationsErrorDescriber.LastNameRequired())
                .Length(2, 16)
                .WithMessage(InformationsErrorDescriber.LastNameLength())
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage(InformationsErrorDescriber.LastNameInvalid())
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage(InformationsErrorDescriber.LastNameUppercase());

            this.RuleFor(request => request.Gender)
                .NotNull()
                .WithMessage(InformationsErrorDescriber.GenderRequired())
                .NotEmpty()
                .WithMessage(InformationsErrorDescriber.GenderRequired());

            //https://stackoverflow.com/questions/7777985/validate-datetime-with-fluentvalidator
            this.RuleFor(request => request.BirthDate)
                .NotNull()
                .WithMessage(InformationsErrorDescriber.BirthDateRequired())
                .NotEmpty()
                .WithMessage(InformationsErrorDescriber.BirthDateRequired())
                .Must(BeAValidDate)
                .WithMessage(InformationsErrorDescriber.BirthDateInvalid());
        }

        private static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}

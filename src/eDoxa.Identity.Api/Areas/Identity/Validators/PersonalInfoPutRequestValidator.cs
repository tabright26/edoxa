// Filename: PersonalInfoPutRequestValidator.cs
// Date Created: 2019-08-22
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Identity.Api.Areas.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public class PersonalInfoPutRequestValidator : AbstractValidator<PersonalInfoPutRequest>
    {
        public PersonalInfoPutRequestValidator()
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
        }
    }
}

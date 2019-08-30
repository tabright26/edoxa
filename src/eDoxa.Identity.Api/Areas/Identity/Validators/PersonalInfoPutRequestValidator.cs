// Filename: PersonalInfoPutRequestValidator.cs
// Date Created: 2019-08-22
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Identity.Api.Areas.Identity.ErrorDescribers;
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
                .WithMessage(PersonalInfoErrorDescriber.FirstNameRequired())
                .NotEmpty()
                .WithMessage(PersonalInfoErrorDescriber.FirstNameRequired())
                .Length(2, 16)
                .WithMessage(PersonalInfoErrorDescriber.FirstNameLength())
                .Matches(new Regex("^[a-zA-Z-]{2,16}$"))
                .WithMessage(PersonalInfoErrorDescriber.FirstNameInvalid())
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage(PersonalInfoErrorDescriber.FirstNameUppercase());
        }
    }
}

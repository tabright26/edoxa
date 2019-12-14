// Filename: InformationsPutRequestValidator.cs
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
    public class InformationsPutRequestValidator : AbstractValidator<InformationsPutRequest>
    {
        public InformationsPutRequestValidator()
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
                .Matches(new Regex("^[A-Z]((-)[a-zA-Z]|[a-z]){1,15}$"))
                .WithMessage(InformationsErrorDescriber.FirstNameUppercase());
        }
    }
}

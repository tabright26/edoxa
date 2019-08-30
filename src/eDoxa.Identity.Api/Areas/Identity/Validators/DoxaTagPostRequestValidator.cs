// Filename: DoxaTagPostRequestValidator.cs
// Date Created: 2019-08-21
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Identity.Api.Areas.Identity.ErrorDescribers;
using eDoxa.Identity.Api.Areas.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public class DoxaTagPostRequestValidator : AbstractValidator<DoxaTagPostRequest>
    {
        public DoxaTagPostRequestValidator()
        {
            this.RuleFor(request => request.Name).NotNull().WithMessage(DoxaTagErrorDescriber.Required()).
                NotEmpty().WithMessage(DoxaTagErrorDescriber.Required()).
                Length(2, 16).WithMessage(DoxaTagErrorDescriber.Length()).
                Matches(new Regex("^[a-zA-Z_]{2,16}$")).
                WithMessage(DoxaTagErrorDescriber.Invalid()).
                Matches(new Regex("^[a-zA-Z][a-zA-Z_]{0,14}[a-zA-Z]$")).
                WithMessage(DoxaTagErrorDescriber.InvalidUnderscore()); ;

        }
    }
}

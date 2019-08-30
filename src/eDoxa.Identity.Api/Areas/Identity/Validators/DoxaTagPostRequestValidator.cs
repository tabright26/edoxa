// Filename: DoxaTagPostRequestValidator.cs
// Date Created: 2019-08-21
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Identity.Api.Areas.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public class DoxaTagPostRequestValidator : AbstractValidator<DoxaTagPostRequest>
    {
        public DoxaTagPostRequestValidator()
        {
            this.RuleFor(request => request.Name).NotNull().WithMessage("DoxaTag is required").
                NotEmpty().WithMessage("DoxaTag is required").
                Length(2, 16).WithMessage("DoxaTag must be between 2 and 16 characters long").
                Matches(new Regex("^[a-zA-Z_]{2,16}$")).
                WithMessage("DoxaTag invalid. May only contains (a-z,A-Z,_)").
                Matches(new Regex("^[a-zA-Z][a-zA-Z_]{0,14}[a-zA-Z]$")).
                WithMessage("DoxaTag invalid. Cannot start or end with _"); ;

        }
    }
}

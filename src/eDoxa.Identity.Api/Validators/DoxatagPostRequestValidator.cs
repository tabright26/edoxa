// Filename: DoxatagPostRequestValidator.cs
// Date Created: 2019-08-21
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Identity.Api.ErrorDescribers;
using eDoxa.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Validators
{
    public class DoxatagPostRequestValidator : AbstractValidator<DoxatagPostRequest>
    {
        public DoxatagPostRequestValidator()
        {
            this.RuleFor(request => request.Name).NotNull().WithMessage(DoxatagErrorDescriber.Required()).
                NotEmpty().WithMessage(DoxatagErrorDescriber.Required()).
                Length(2, 16).WithMessage(DoxatagErrorDescriber.Length()).
                Matches(new Regex("^[a-zA-Z_]{2,16}$")).
                WithMessage(DoxatagErrorDescriber.Invalid()).
                Matches(new Regex("^[a-zA-Z][a-zA-Z_]{0,14}[a-zA-Z]$")).
                WithMessage(DoxatagErrorDescriber.InvalidUnderscore()); ;

        }
    }
}

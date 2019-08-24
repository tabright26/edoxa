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
            this.RuleFor(request => request.Name).NotNull().NotEmpty().Length(2, 16).Matches(new Regex("^[0-9a-zA-Z_]*$"));
        }
    }
}

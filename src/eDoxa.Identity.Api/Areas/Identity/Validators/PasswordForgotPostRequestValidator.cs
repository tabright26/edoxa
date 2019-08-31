﻿// Filename: DoxaTagPostRequestValidator.cs
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
    public class PasswordForgotPostRequestValidator : AbstractValidator<PasswordForgotPostRequest>
    {
        public PasswordForgotPostRequestValidator()
        {
            this.RuleFor(request => request.Email).NotNull().WithMessage(PasswordForgotErrorDescriber.EmailRequired()).
                NotEmpty().WithMessage(PasswordForgotErrorDescriber.EmailRequired()).
                Matches(new Regex("^([A-Z|a-z|0-9](\\.|_){0,1})+[A-Z|a-z|0-9]\\@([A-Z|a-z|0-9])+((\\.){0,1}[A-Z|a-z|0-9]){2}\\.[a-z]{2,3}$")).
                WithMessage(PasswordForgotErrorDescriber.EmailInvalid());

        }
    }
}
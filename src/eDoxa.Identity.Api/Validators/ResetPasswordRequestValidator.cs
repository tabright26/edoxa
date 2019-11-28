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
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            this.RuleFor(request => request.Email).NotNull().WithMessage(PasswordResetErrorDescriber.EmailRequired()).
                NotEmpty().WithMessage(PasswordForgotErrorDescriber.EmailRequired()).
                Matches(new Regex("^([A-Z|a-z|0-9](\\.|_){0,1})+[A-Z|a-z|0-9]\\@([A-Z|a-z|0-9])+((\\.){0,1}[A-Z|a-z|0-9]){2}\\.[a-z]{2,3}$")).
                WithMessage(PasswordForgotErrorDescriber.EmailInvalid());

            this.RuleFor(request => request.Password).NotNull().WithMessage(PasswordResetErrorDescriber.PasswordRequired()).
                NotEmpty().WithMessage(PasswordResetErrorDescriber.PasswordRequired()).
                MinimumLength(8).WithMessage(PasswordResetErrorDescriber.PasswordLength()).
                Matches(new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")).
                WithMessage(PasswordResetErrorDescriber.PasswordInvalid()).
                Matches(new Regex("^((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,})$")).
                WithMessage(PasswordResetErrorDescriber.PasswordSpecial());

        }
    }
}

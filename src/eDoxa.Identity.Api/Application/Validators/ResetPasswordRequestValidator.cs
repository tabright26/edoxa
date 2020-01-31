// Filename: ResetPasswordRequestValidator.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.ErrorDescribers;

using FluentValidation;

namespace eDoxa.Identity.Api.Application.Validators
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            this.RuleFor(request => request.Email).EmailAddress().WithMessage("Email is invalid");

            this.RuleFor(request => request.Password)
                .NotNull()
                .WithMessage(PasswordResetErrorDescriber.PasswordRequired())
                .NotEmpty()
                .WithMessage(PasswordResetErrorDescriber.PasswordRequired())
                .MinimumLength(8)
                .WithMessage(PasswordResetErrorDescriber.PasswordLength())
                .Matches(new Regex("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
                .WithMessage(PasswordResetErrorDescriber.PasswordInvalid())
                .Matches(new Regex("^((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,})$"))
                .WithMessage(PasswordResetErrorDescriber.PasswordSpecial());
        }
    }
}

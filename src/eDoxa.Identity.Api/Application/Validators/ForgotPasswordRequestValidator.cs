﻿// Filename: ForgotPasswordRequestValidator.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.ErrorDescribers;

using FluentValidation;

namespace eDoxa.Identity.Api.Application.Validators
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            this.RuleFor(request => request.Email)
                .NotNull()
                .WithMessage(PasswordForgotErrorDescriber.EmailRequired())
                .NotEmpty()
                .WithMessage(PasswordForgotErrorDescriber.EmailRequired())
                .Matches(new Regex("^([A-Z|a-z|0-9](\\.|_){0,1})+[A-Z|a-z|0-9]\\@([A-Z|a-z|0-9])+((\\.){0,1}[A-Z|a-z|0-9]){2}\\.[a-z]{2,3}$"))
                .WithMessage(PasswordForgotErrorDescriber.EmailInvalid());
        }
    }
}
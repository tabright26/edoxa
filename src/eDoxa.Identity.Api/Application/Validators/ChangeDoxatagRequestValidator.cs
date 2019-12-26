// Filename: ChangeDoxatagRequestValidator.cs
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
    public class ChangeDoxatagRequestValidator : AbstractValidator<ChangeDoxatagRequest>
    {
        public ChangeDoxatagRequestValidator()
        {
            this.RuleFor(request => request.Name)
                .NotNull()
                .WithMessage(DoxatagErrorDescriber.Required())
                .NotEmpty()
                .WithMessage(DoxatagErrorDescriber.Required())
                .Length(2, 16)
                .WithMessage(DoxatagErrorDescriber.Length())
                .Matches(new Regex("^[a-zA-Z0-9_ -]{2,16}$"))
                .WithMessage(DoxatagErrorDescriber.Invalid())
                .Matches(new Regex("^[a-zA-Z0-9][a-zA-Z0-9_ -]{0,14}[a-zA-Z0-9_-]$"))
                .WithMessage(DoxatagErrorDescriber.InvalidUnderscore());

            ;
        }
    }
}

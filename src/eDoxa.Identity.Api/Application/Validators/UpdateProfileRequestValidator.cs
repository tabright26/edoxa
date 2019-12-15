// Filename: InformationsPutRequestValidator.cs
// Date Created: 2019-08-22
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.ErrorDescribers;

using FluentValidation;

namespace eDoxa.Identity.Api.Application.Validators
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator()
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
                .Matches(new Regex("^[A-Z](((-)[A-Z])|[a-z]){1,15}$"))
                .WithMessage(InformationsErrorDescriber.FirstNameUppercase());
        }
    }
}

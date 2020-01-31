// Filename: RegisterAccountRequestValidator.cs
// Date Created: 2020-01-31
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Globalization;

using eDoxa.Grpc.Protos.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Application.Validators
{
    public sealed class RegisterAccountRequestValidator : AbstractValidator<RegisterAccountRequest>
    {
        public RegisterAccountRequestValidator()
        {
            this.RuleFor(request => request.Email).EmailAddress().WithMessage("Email is invalid");

            this.RuleFor(request => request.Dob)
                .Custom(
                    (dob, context) =>
                    {
                        var formats = new[] {"MM/dd/yyyy", "MM/d/yyyy", "M/d/yyyy", "M/dd/yyyy"};

                        if (DateTime.TryParseExact(
                            dob,
                            formats,
                            new CultureInfo("en-US"),
                            DateTimeStyles.None,
                            out var value))
                        {
                            if (DateTime.Today.Year - value.Year < 18)
                            {
                                context.AddFailure(nameof(dob), "You must be 18 years old or older");
                            }
                        }
                        else
                        {
                            context.AddFailure(nameof(dob), "Invalid date of birth");
                        }
                    });
        }
    }
}

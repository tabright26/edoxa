// Filename: AddressPutRequestValidator.cs
// Date Created: 2019-08-23
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Identity.Api.Areas.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public class AddressPutRequestValidator : AbstractValidator<AddressPutRequest>
    {
        public AddressPutRequestValidator()
        {
            this.RuleFor(request => request.Line1)
                .NotNull()
                .WithMessage("Main address is required")
                .NotEmpty()
                .WithMessage("Main address is required")
                .Matches(new Regex("^[a-zA-Z0-9- .,]{1,}$"))
                .WithMessage("Main address invalid. Must not have special characters");

            this.RuleFor(request => request.Line2)
                .Matches(new Regex("^[a-zA-Z0-9- .,]{1,}$"))
                .WithMessage("Secondary address invalid. Must not have special characters");

            this.RuleFor(request => request.City)
                .NotNull()
                .WithMessage("City is required")
                .NotEmpty()
                .WithMessage("City is required")
                .Matches(new Regex("^[a-zA-Z- ]{1,}$"))
                .WithMessage("City is invalid. Only letters, spaces and hyphens allowed");

            this.RuleFor(request => request.State)
                .NotNull()
                .WithMessage("State is required")
                .NotEmpty()
                .WithMessage("State is required")
                .Matches(new Regex("^[a-zA-Z- ]{1,}$"))
                .WithMessage("State is invalid. Only letters, spaces and hyphens allowed");

            this.RuleFor(request => request.PostalCode)
                .NotNull()
                .WithMessage("Postal code is required")
                .NotEmpty()
                .WithMessage("Postal code is required")
                .Length(5, 6).WithMessage("Postal code must be between 5 and 6 characters long")
                .Matches(new Regex("^[0-9A-Z]{5,6}$"))
                .WithMessage("Postal code is invalid. Only letters and numbers allowed");
        }
    }
}

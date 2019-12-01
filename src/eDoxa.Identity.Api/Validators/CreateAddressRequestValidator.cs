// Filename: AddressPostRequestValidator.cs
// Date Created: 2019-08-23
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.RegularExpressions;

using eDoxa.Identity.Api.ErrorDescribers;
using eDoxa.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Validators
{
    public sealed class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
    {
        public CreateAddressRequestValidator()
        {
            //this.Enumeration(request => request.Country).NotEmpty().NotAll().IsInEnumeration(); // TODO: Need to be fixed.

            this.RuleFor(request => request.Line1)
                .NotNull()
                .WithMessage(AddressBookErrorDescriber.Line1Required())
                .NotEmpty()
                .WithMessage(AddressBookErrorDescriber.Line1Required())
                .Matches(new Regex("^[a-zA-Z0-9- .,]{1,}$"))
                .WithMessage(AddressBookErrorDescriber.Line1Invalid());

            this.RuleFor(request => request.Line2)
                .Matches(new Regex("^[a-zA-Z0-9- .,]{1,}$"))
                .WithMessage(AddressBookErrorDescriber.Line2Invalid());

            this.RuleFor(request => request.City)
                .NotNull()
                .WithMessage(AddressBookErrorDescriber.CityRequired())
                .NotEmpty()
                .WithMessage(AddressBookErrorDescriber.CityRequired())
                .Matches(new Regex("^[a-zA-Z- ]{1,}$"))
                .WithMessage(AddressBookErrorDescriber.CityInvalid());

            this.RuleFor(request => request.State)
                .NotNull()
                .WithMessage(AddressBookErrorDescriber.StateRequired())
                .NotEmpty()
                .WithMessage(AddressBookErrorDescriber.StateRequired())
                .Matches(new Regex("^[a-zA-Z- ]{1,}$"))
                .WithMessage(AddressBookErrorDescriber.StateInvalid());

            this.RuleFor(request => request.PostalCode)
                .NotNull()
                .WithMessage(AddressBookErrorDescriber.PostalCodeRequired())
                .NotEmpty()
                .WithMessage(AddressBookErrorDescriber.PostalCodeRequired())
                .Length(5, 6).WithMessage(AddressBookErrorDescriber.PostalCodeLength())
                .Matches(new Regex("^[0-9A-Z]{5,6}$"))
                .WithMessage(AddressBookErrorDescriber.PostalCodeInvalidError());
        }
    }
}
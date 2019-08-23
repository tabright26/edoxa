// Filename: AddressPostRequestValidator.cs
// Date Created: 2019-08-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public sealed class AddressPostRequestValidator : AbstractValidator<AddressPostRequest>
    {
        public AddressPostRequestValidator()
        {
            this.RuleFor(request => request.Country).NotNull().NotEmpty();
            this.RuleFor(request => request.Line1).NotNull().NotEmpty();
            this.RuleFor(request => request.Line2).NotEmpty().When(request => request.Line2 != null);
            this.RuleFor(request => request.City).NotNull().NotEmpty();
            this.RuleFor(request => request.State).NotEmpty().When(request => request.State != null);
            this.RuleFor(request => request.PostalCode).NotEmpty().When(request => request.PostalCode != null);
        }
    }
}

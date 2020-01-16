// Filename: UpdateAddressRequestValidator.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.Options;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Seedwork.Application.FluentValidation.Extensions;

using FluentValidation;

using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Validators
{
    public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
    {
        public UpdateAddressRequestValidator(IOptions<IdentityStaticOptions> options)
        {
            this.RuleFor(request => request.Country)
                .NotNull()
                .NotEmpty()
                .IsInEnum()
                .Must(country => country != EnumCountry.None && country != EnumCountry.All)
                .DependentRules(
                    () =>
                    {
                        var addressOptions = options.Value.Default.Address;
                        var fieldsOptions = addressOptions.Fields;
                        var validatorOptions = addressOptions.Validator;

                        this.RuleFor(request => request.Line1).Custom(validatorOptions.Line1);

                        this.When(
                            request => !addressOptions.Fields.Line2.Excluded && !string.IsNullOrWhiteSpace(request.Line2),
                            () => this.RuleFor(request => request.Line2).Custom(validatorOptions.Line2));

                        this.RuleFor(request => request.City).Custom(validatorOptions.City);

                        this.When(request => !fieldsOptions.State.Excluded, () => this.RuleFor(request => request.State).Custom(validatorOptions.State));

                        this.When(
                            request => !fieldsOptions.PostalCode.Excluded,
                            () => this.RuleFor(request => request.PostalCode).Custom(validatorOptions.PostalCode));
                    });
        }
    }
}

﻿// Filename: CreateAddressRequestValidator.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.Options;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Seedwork.Application.FluentValidation.Extensions;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Validators
{
    public sealed class CreateAddressRequestValidator : AbstractValidator<CreateAddressRequest>
    {
        private readonly IOptions<IdentityApiOptions> _optionsSnapshot;

        public CreateAddressRequestValidator(IOptionsSnapshot<IdentityApiOptions> optionsSnapshot)
        {
            _optionsSnapshot = optionsSnapshot;

            this.RuleFor(request => request.CountryIsoCode)
                .NotNull()
                .NotEmpty()
                .IsInEnum()
                .Must(countryIsoCode => countryIsoCode != EnumCountryIsoCode.None && countryIsoCode != EnumCountryIsoCode.All)
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(request => request)
                            .Custom(
                                (x, context) =>
                                {
                                    var fieldsOptions =
                                        (IdentityApiOptions.Types.AddressOptions.Types.FieldsOptions) context.ParentContext.RootContextData["FieldsOptions"];

                                    var validatorOptions =
                                        (IdentityApiOptions.Types.AddressOptions.Types.ValidatorOptions) context.ParentContext.RootContextData[
                                            "ValidatorOptions"];

                                    context.ValidateCustomRule(nameof(x.Line1), x.Line1, validatorOptions.Line1);

                                    if (!fieldsOptions.Line2.Excluded)
                                    {
                                        context.ValidateCustomRule(nameof(x.Line2), x.Line2, validatorOptions.Line2);
                                    }

                                    context.ValidateCustomRule(nameof(x.City), x.City, validatorOptions.City);

                                    if (!fieldsOptions.State.Excluded)
                                    {
                                        context.ValidateCustomRule(nameof(x.State), x.State, validatorOptions.State);
                                    }

                                    if (!fieldsOptions.PostalCode.Excluded)
                                    {
                                        context.ValidateCustomRule(nameof(x.PostalCode), x.PostalCode, validatorOptions.PostalCode);
                                    }
                                });
                    });
        }

        protected override bool PreValidate(ValidationContext<CreateAddressRequest> context, ValidationResult result)
        {
            var addressOptions = _optionsSnapshot.Value.TryOverridesAddressOptionsFor(context.InstanceToValidate.CountryIsoCode);

            context.RootContextData.Add("FieldsOptions", addressOptions.Fields);

            context.RootContextData.Add("ValidatorOptions", addressOptions.Validator);

            return base.PreValidate(context, result);
        }
    }
}

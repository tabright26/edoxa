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
using FluentValidation.Results;

using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Validators
{
    public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
    {
        private readonly IOptions<IdentityStaticOptions> _options;

        public UpdateAddressRequestValidator(IOptions<IdentityStaticOptions> options)
        {
            _options = options;

            this.RuleFor(request => request.Country)
                .NotNull()
                .NotEmpty()
                .IsInEnum()
                .Must(country => country != EnumCountry.None && country != EnumCountry.All)
                .DependentRules(
                    () =>
                    {
                        this.RuleFor(request => request)
                            .Custom(
                                (x, context) =>
                                {
                                    var fieldsOptions =
                                        (IdentityStaticOptions.Types.AddressOptions.Types.FieldsOptions) context.ParentContext.RootContextData["FieldsOptions"];

                                    var validatorOptions =
                                        (IdentityStaticOptions.Types.AddressOptions.Types.ValidatorOptions) context.ParentContext.RootContextData[
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

        protected override bool PreValidate(ValidationContext<UpdateAddressRequest> context, ValidationResult result)
        {
            var addressOptions = _options.Value.GetAddressOptionsFor(context.InstanceToValidate.Country);

            context.RootContextData.Add("FieldsOptions", addressOptions.Fields);

            context.RootContextData.Add("ValidatorOptions", addressOptions.Validator);

            return base.PreValidate(context, result);
        }
    }
}

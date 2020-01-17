// Filename: IdentityApiOptions.cs
// Date Created: 2020-01-14
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Grpc.Protos.Identity.Enums;

using static eDoxa.Grpc.Protos.Identity.Options.IdentityApiOptions.Types;
using static eDoxa.Grpc.Protos.Identity.Options.IdentityApiOptions.Types.AddressOptions.Types;
using static eDoxa.Grpc.Protos.Identity.Options.IdentityApiOptions.Types.AddressOptions.Types.FieldsOptions.Types;

namespace eDoxa.Grpc.Protos.Identity.Options
{
    public partial class IdentityApiOptions
    {
        public IEnumerable<string> GetLine1ErrorsFor(EnumCountryIsoCode countryIsoCode) // TODO: TO REFACTOR
        {
            return GetFieldValidationRuleErrorMessages(this.TryOverridesAddressOptionsFor(countryIsoCode).Validator.Line1);
        }

        public IEnumerable<string> GetLine2ErrorsFor(EnumCountryIsoCode countryIsoCode) // TODO: TO REFACTOR
        {
            return GetFieldValidationRuleErrorMessages(this.TryOverridesAddressOptionsFor(countryIsoCode).Validator.Line2);
        }

        public IEnumerable<string> GetCityErrorsFor(EnumCountryIsoCode countryIsoCode) // TODO: TO REFACTOR
        {
            return GetFieldValidationRuleErrorMessages(this.TryOverridesAddressOptionsFor(countryIsoCode).Validator.City);
        }

        public IEnumerable<string> GetStateErrorsFor(EnumCountryIsoCode countryIsoCode) // TODO: TO REFACTOR
        {
            return GetFieldValidationRuleErrorMessages(this.TryOverridesAddressOptionsFor(countryIsoCode).Validator.State);
        }

        public IEnumerable<string> GetPostalCodeErrorsFor(EnumCountryIsoCode countryIsoCode) // TODO: TO REFACTOR
        {
            return GetFieldValidationRuleErrorMessages(this.TryOverridesAddressOptionsFor(countryIsoCode).Validator.PostalCode);
        }

        public static IEnumerable<string> GetFieldValidationRuleErrorMessages(IEnumerable<FieldValidationRule> validationRules) // TODO: TO REFACTOR
        {
            return validationRules.Where(validationRule => validationRule.Enabled)
                .OrderBy(validationRule => validationRule.Priority)
                .Select(validationRule => string.Format(validationRule.Message, validationRule.Value))
                .ToList();
        }

        public AddressOptions TryOverridesAddressOptionsFor(EnumCountryIsoCode countryIsoCode)
        {
            var addressOptions = Static.Countries.SingleOrDefault(country => country.IsoCode == countryIsoCode)?.Address;

            return new AddressOptions
            {
                Fields = new FieldsOptions
                {
                    Country = new CountryFieldOptions
                    {
                        Label = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.Country?.Label)
                            ? addressOptions.Fields?.Country?.Label
                            : Default.Address.Fields.Country.Label,
                        Placeholder = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.Country?.Placeholder)
                            ? addressOptions.Fields?.Country?.Placeholder
                            : Default.Address.Fields.Country.Placeholder
                    },
                    Line1 = new Line1FieldOptions
                    {
                        Label = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.Line1?.Label)
                            ? addressOptions.Fields?.Line1?.Label
                            : Default.Address.Fields.Line1.Label,
                        Placeholder = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.Line1?.Placeholder)
                            ? addressOptions.Fields?.Line1?.Placeholder
                            : Default.Address.Fields.Line1.Placeholder
                    },
                    Line2 = new Line2FieldOptions
                    {
                        Label = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.Line2?.Label)
                            ? addressOptions.Fields?.Line2?.Label
                            : Default.Address.Fields.Line2.Label,
                        Placeholder = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.Line2?.Placeholder)
                            ? addressOptions.Fields?.Line2?.Placeholder
                            : Default.Address.Fields.Line2.Placeholder,
                        Excluded = addressOptions?.Fields?.Line2?.Excluded ?? Default.Address.Fields.Line2.Excluded
                    },
                    City = new CityFieldOptions
                    {
                        Label = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.City?.Label)
                            ? addressOptions.Fields?.City?.Label
                            : Default.Address.Fields.City.Label,
                        Placeholder = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.City?.Placeholder)
                            ? addressOptions.Fields?.City?.Placeholder
                            : Default.Address.Fields.City.Placeholder
                    },
                    State = new StateFieldOptions
                    {
                        Label = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.State?.Label)
                            ? addressOptions.Fields?.State?.Label
                            : Default.Address.Fields.State.Label,
                        Placeholder = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.State?.Placeholder)
                            ? addressOptions?.Fields?.State?.Placeholder
                            : Default.Address.Fields.State.Placeholder,
                        Excluded = addressOptions?.Fields?.State?.Excluded ?? Default.Address.Fields.State.Excluded
                    },
                    PostalCode = new PostalCodeFieldOptions
                    {
                        Label = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.PostalCode?.Label)
                            ? addressOptions?.Fields?.PostalCode?.Label
                            : Default.Address.Fields.PostalCode.Label,
                        Placeholder = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.PostalCode?.Placeholder)
                            ? addressOptions?.Fields?.PostalCode?.Placeholder
                            : Default.Address.Fields.PostalCode.Placeholder,
                        Excluded = addressOptions?.Fields?.PostalCode?.Excluded ?? Default.Address.Fields.PostalCode.Excluded,
                        Mask = !string.IsNullOrWhiteSpace(addressOptions?.Fields?.PostalCode?.Mask)
                            ? addressOptions?.Fields?.PostalCode?.Mask
                            : Default.Address.Fields.PostalCode.Mask
                    }
                },
                Validator = new ValidatorOptions
                {
                    Line1 =
                    {
                        addressOptions?.Validator?.Line1?.Any() ?? false ? addressOptions?.Validator?.Line1 : Default.Address.Validator.Line1
                    },
                    Line2 =
                    {
                        addressOptions?.Validator?.Line2?.Any() ?? false ? addressOptions?.Validator?.Line2 : Default.Address.Validator.Line2
                    },
                    City =
                    {
                        addressOptions?.Validator?.City?.Any() ?? false ? addressOptions?.Validator?.City : Default.Address.Validator.City
                    },
                    State =
                    {
                        addressOptions?.Validator?.State?.Any() ?? false ? addressOptions?.Validator?.State : Default.Address.Validator.State
                    },
                    PostalCode =
                    {
                        addressOptions?.Validator?.PostalCode?.Any() ?? false ? addressOptions?.Validator?.PostalCode : Default.Address.Validator.PostalCode
                    }
                }
            };
        }
    }
}

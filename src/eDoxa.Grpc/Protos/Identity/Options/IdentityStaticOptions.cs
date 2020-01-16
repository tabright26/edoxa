// Filename: IdentityStaticOptions.cs
// Date Created: 2020-01-14
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Grpc.Protos.Identity.Enums;

namespace eDoxa.Grpc.Protos.Identity.Options
{
    public partial class IdentityStaticOptions
    {
        public IEnumerable<string> GetLine1ErrorsFor(EnumCountry country)
        {
            return GetFieldValidationRuleErrorMessages(this.GetAddressOptionsFor(country).Validator.Line1);
        }

        public IEnumerable<string> GetLine2ErrorsFor(EnumCountry country)
        {
            return GetFieldValidationRuleErrorMessages(this.GetAddressOptionsFor(country).Validator.Line2);
        }

        public IEnumerable<string> GetCityErrorsFor(EnumCountry country)
        {
            return GetFieldValidationRuleErrorMessages(this.GetAddressOptionsFor(country).Validator.City);
        }

        public IEnumerable<string> GetStateErrorsFor(EnumCountry country)
        {
            return GetFieldValidationRuleErrorMessages(this.GetAddressOptionsFor(country).Validator.State);
        }

        public IEnumerable<string> GetPostalCodeErrorsFor(EnumCountry country)
        {
            return GetFieldValidationRuleErrorMessages(this.GetAddressOptionsFor(country).Validator.PostalCode);
        }

        public static IEnumerable<string> GetFieldValidationRuleErrorMessages(IEnumerable<FieldValidationRule> validationRules)
        {
            return validationRules.Where(validationRule => validationRule.Enabled)
                .OrderBy(validationRule => validationRule.Order)
                .Select(validationRule => string.Format(validationRule.Message, validationRule.Value))
                .ToList();
        }

        public Types.AddressOptions GetAddressOptionsFor(EnumCountry country)
        {
            var addressOptions = AddressBook.Countries.SingleOrDefault(countryOptions => countryOptions.TwoIso == country.ToString())?.Address;

            return new Types.AddressOptions
            {
                Fields = new Types.AddressOptions.Types.FieldsOptions
                {
                    Country = new Types.AddressOptions.Types.FieldsOptions.Types.CountryFieldOptions
                    {
                        Label = addressOptions?.Fields?.Country?.Label ?? Default.Address.Fields.Country.Label,
                        Placeholder = addressOptions?.Fields?.Country?.Placeholder ?? Default.Address.Fields.Country.Placeholder
                    },
                    Line1 = new Types.AddressOptions.Types.FieldsOptions.Types.Line1FieldOptions
                    {
                        Label = addressOptions?.Fields?.Line1?.Label ?? Default.Address.Fields.Line1.Label,
                        Placeholder = addressOptions?.Fields?.Line1?.Placeholder ?? Default.Address.Fields.Line1.Placeholder
                    },
                    Line2 = new Types.AddressOptions.Types.FieldsOptions.Types.Line2FieldOptions
                    {
                        Label = addressOptions?.Fields?.Line2?.Label ?? Default.Address.Fields.Line2.Label,
                        Placeholder = addressOptions?.Fields?.Line2?.Placeholder ?? Default.Address.Fields.Line2.Placeholder,
                        Excluded = addressOptions?.Fields?.Line2?.Excluded ?? Default.Address.Fields.Line2.Excluded
                    },
                    City = new Types.AddressOptions.Types.FieldsOptions.Types.CityFieldOptions
                    {
                        Label = addressOptions?.Fields?.City?.Label ?? Default.Address.Fields.City.Label,
                        Placeholder = addressOptions?.Fields?.City?.Placeholder ?? Default.Address.Fields.City.Placeholder
                    },
                    State = new Types.AddressOptions.Types.FieldsOptions.Types.StateFieldOptions
                    {
                        Label = addressOptions?.Fields?.State?.Label ?? Default.Address.Fields.State.Label,
                        Placeholder = addressOptions?.Fields?.State?.Placeholder ?? Default.Address.Fields.State.Placeholder,
                        Excluded = addressOptions?.Fields?.State?.Excluded ?? Default.Address.Fields.State.Excluded
                    },
                    PostalCode = new Types.AddressOptions.Types.FieldsOptions.Types.PostalCodeFieldOptions
                    {
                        Label = addressOptions?.Fields?.PostalCode?.Label ?? Default.Address.Fields.PostalCode.Label,
                        Placeholder = addressOptions?.Fields?.PostalCode?.Placeholder ?? Default.Address.Fields.PostalCode.Placeholder,
                        Excluded = addressOptions?.Fields?.PostalCode?.Excluded ?? Default.Address.Fields.PostalCode.Excluded,
                        Mask = addressOptions?.Fields?.PostalCode?.Mask ?? Default.Address.Fields.PostalCode.Mask
                    }
                },
                Validator = new Types.AddressOptions.Types.ValidatorOptions
                {
                    Line1 =
                    {
                        addressOptions?.Validator?.Line1 ?? Default.Address.Validator.Line1
                    },
                    Line2 =
                    {
                        addressOptions?.Validator?.Line2 ?? Default.Address.Validator.Line2
                    },
                    City =
                    {
                        addressOptions?.Validator?.City ?? Default.Address.Validator.City
                    },
                    State =
                    {
                        addressOptions?.Validator?.State ?? Default.Address.Validator.State
                    },
                    PostalCode =
                    {
                        addressOptions?.Validator?.PostalCode ?? Default.Address.Validator.PostalCode
                    }
                }
            };
        }
    }
}

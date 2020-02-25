// Filename: CustomContextExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using eDoxa.Grpc.Protos.CustomTypes;

using FluentValidation.Validators;

namespace eDoxa.Seedwork.Application.FluentValidation.Extensions
{
    public static class CustomContextExtensions
    {
        public static void ValidateCustomRule(
            this CustomContext context,
            string propertyName,
            string propertyValue,
            IEnumerable<FieldValidationRule> validationRules
        )
        {
            foreach (var validationRule in validationRules.Where(validationRule => validationRule.Enabled).OrderBy(validationRule => validationRule.Priority))
            {
                if (!validationRule.IsValid(propertyValue))
                {
                    context.AddFailure(propertyName, string.Format(validationRule.Message, validationRule.Value));
                }
            }
        }

        private static bool IsValid(this FieldValidationRule validationRule, string value)
        {
            switch (validationRule.Type)
            {
                case FieldValidationRuleType.Required:
                {
                    return !string.IsNullOrWhiteSpace(value);
                }

                case FieldValidationRuleType.Regex:
                {
                    return string.IsNullOrWhiteSpace(value) || new Regex(validationRule.Value).IsMatch(value);
                }

                case FieldValidationRuleType.Length:
                {
                    return value.Length == Convert.ToInt32(validationRule.Value);
                }

                case FieldValidationRuleType.MinLength:
                {
                    return value.Length >= Convert.ToInt32(validationRule.Value);
                }

                case FieldValidationRuleType.MaxLength:
                {
                    return value.Length <= Convert.ToInt32(validationRule.Value);
                }

                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}

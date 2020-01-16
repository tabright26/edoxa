// Filename: ValidatorBuilderExtenisons.cs
// Date Created: 2020-01-15
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Grpc.Protos.CustomTypes;

using FluentValidation;

namespace eDoxa.Seedwork.Application.FluentValidation.Extensions
{
    public static class ValidatorBuilderExtenisons
    {
        public static void Custom<T>(this IRuleBuilder<T, string> ruleBuilder, IEnumerable<FieldValidationRule> validationRules)
        {
            foreach (var validationRule in validationRules.Where(validationRule => validationRule.Enabled).OrderBy(validationRule => validationRule.Order))
            {
                ruleBuilder.Custom(validationRule).WithMessage(string.Format(validationRule.Message, validationRule.Value));
            }
        }

        private static IRuleBuilderOptions<T, string> Custom<T>(this IRuleBuilder<T, string> ruleBuilder, FieldValidationRule validationRule)
        {
            switch (validationRule.Type)
            {
                case FieldValidationRuleType.Required:
                {
                    return ruleBuilder.Must(value => !string.IsNullOrWhiteSpace(value));
                }

                case FieldValidationRuleType.Regex:
                {
                    return ruleBuilder.Matches(validationRule.Value);
                }

                case FieldValidationRuleType.Length:
                {
                    return ruleBuilder.Length(Convert.ToInt32(validationRule.Value));
                }

                case FieldValidationRuleType.MinLength:
                {
                    return ruleBuilder.MinimumLength(Convert.ToInt32(validationRule.Value));
                }

                case FieldValidationRuleType.MaxLength:
                {
                    return ruleBuilder.MaximumLength(Convert.ToInt32(validationRule.Value));
                }

                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}

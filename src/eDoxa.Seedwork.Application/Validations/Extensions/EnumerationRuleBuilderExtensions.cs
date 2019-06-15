// Filename: EnumerationRuleBuilderExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

using FluentValidation;

namespace eDoxa.Seedwork.Application.Validations.Extensions
{
    public static class EnumerationRuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TEnumeration> NotNull<T, TEnumeration>(this IRuleBuilder<T, TEnumeration> validator)
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return DefaultValidatorExtensions.NotNull(validator).WithMessage($"The enumeration {typeof(TEnumeration).Name} is required.");
        }

        public static IRuleBuilderOptions<T, TEnumeration> NotAll<T, TEnumeration>(this IRuleBuilder<T, TEnumeration> validator, bool allowOnly = false)
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return validator.Must(enumeration => enumeration != Enumeration<TEnumeration>.All)
                .WithMessage(
                    $"The enumeration {typeof(TEnumeration).Name} cannot be All (-1). These are valid enumeration names: [{string.Join(", ", Enumeration<TEnumeration>.GetEnumerations(allowOnly))}]."
                );
        }

        public static IRuleBuilderOptions<T, TEnumeration> IsInEnumeration<T, TEnumeration>(this IRuleBuilder<T, TEnumeration> validator, bool allowOnly = false)
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return validator.Must(enumeration => Enumeration<TEnumeration>.HasEnumeration(enumeration, allowOnly))
                .WithMessage(
                    $"The enumeration {typeof(TEnumeration).Name} is invalid. These are valid enumeration names: [{string.Join(", ", Enumeration<TEnumeration>.GetEnumerations(allowOnly))}]."
                );
        }
    }
}

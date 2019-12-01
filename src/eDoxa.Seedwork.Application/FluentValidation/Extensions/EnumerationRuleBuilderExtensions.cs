// Filename: EnumerationRuleBuilderExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.FluentValidation.ErrorDescribers;
using eDoxa.Seedwork.Domain;

using FluentValidation;

namespace eDoxa.Seedwork.Application.FluentValidation.Extensions
{
    public static class EnumerationRuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TEnumeration> NotNull<T, TEnumeration>(this IRuleBuilder<T, TEnumeration> validator)
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return DefaultValidatorExtensions.NotNull(validator).WithMessage(EnumerationErrorDescribers.NotNull<TEnumeration>());
        }

        public static IRuleBuilderOptions<T, TEnumeration> NotAll<T, TEnumeration>(this IRuleBuilder<T, TEnumeration> validator, bool allowOnly = false)
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return validator.Must(enumeration => enumeration != Enumeration<TEnumeration>.All).WithMessage(EnumerationErrorDescribers.NotAll<TEnumeration>());
        }

        public static IRuleBuilderOptions<T, TEnumeration> IsInEnumeration<T, TEnumeration>(this IRuleBuilder<T, TEnumeration> validator)
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return validator.Must(Enumeration<TEnumeration>.HasEnumeration).WithMessage(EnumerationErrorDescribers.IsInEnumeration<TEnumeration>());
        }
    }
}

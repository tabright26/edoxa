// Filename: ValidationExtensions.cs
// Date Created: 2019-05-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using FluentValidation;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> IsInEnumeration<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string propertyName)
        where TProperty : IEnumeration
        {
            return ruleBuilder.Must(Enumeration.IsInEnumeration).WithMessage($"The {propertyName} property is invalid. These are valid input values: {Enumeration.DisplayNames<TProperty>()}.");
        }

        public static IRuleBuilderOptions<T, TProperty> NotEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string propertyName)
        where TProperty : EntityId<TProperty>, new()
        {
            return ruleBuilder.Must(entityId => !entityId.IsTransient()).WithMessage($"The {propertyName} property is invalid.");
        }
    }
}

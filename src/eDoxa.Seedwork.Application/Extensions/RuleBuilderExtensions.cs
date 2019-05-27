// Filename: ValidationExtensions.cs
// Date Created: 2019-05-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using FluentValidation;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static void RuleForEntityId<T, TEntityId>(this AbstractValidator<T> validator, Expression<Func<T, TEntityId>> expression)
        where TEntityId : EntityId<TEntityId>, new()
        {
            validator.RuleFor(expression).NotEmpty().Must(entityId => !entityId.IsTransient()).WithMessage($"The ID {typeof(TEntityId).Name} is invalid.");
        }

        public static void RuleForEnumeration<T, TEnumeration>(this AbstractValidator<T> validator, Expression<Func<T, TEnumeration>> expression)
        where TEnumeration : IEnumeration
        {
            validator.RuleFor(expression).Must(Enumeration.IsInEnumeration).WithMessage($"The enumeration {typeof(TEnumeration).Name} is invalid. These are valid enumeration names: {Enumeration.DisplayNames<TEnumeration>()}.");
        }
    }
}

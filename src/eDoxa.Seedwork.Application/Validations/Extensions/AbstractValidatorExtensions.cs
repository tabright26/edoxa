﻿// Filename: AbstractValidatorExtensions.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using eDoxa.Seedwork.Domain.Aggregate;

using FluentValidation;

namespace eDoxa.Seedwork.Application.Validations.Extensions
{
    public static class AbstractValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TEntityId> EntityId<T, TEntityId>(this AbstractValidator<T> validator, Expression<Func<T, TEntityId>> expression)
        where TEntityId : EntityId<TEntityId>, new()
        {
            return validator.RuleFor(expression).NotNull().DependentRules(() => validator.RuleFor(expression).NotEmpty());
        }

        public static IRuleBuilderOptions<T, TEnumeration> Enumeration<T, TEnumeration>(
            this AbstractValidator<T> validator,
            Expression<Func<T, TEnumeration>> expression
        )
        where TEnumeration : Enumeration<TEnumeration>, new()
        {
            return validator.RuleFor(expression)
                .NotNull()
                .DependentRules(() => validator.RuleFor(expression).NotAll().DependentRules(() => validator.RuleFor(expression).IsInEnumeration()));
        }
    }
}

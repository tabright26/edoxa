// Filename: AbstractValidatorExtensions.cs
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

using eDoxa.Seedwork.Domain.Aggregate;

using FluentValidation;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class AbstractValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TEntityId> EntityId<T, TEntityId>(this AbstractValidator<T> validator, Expression<Func<T, TEntityId>> expression)
        where TEntityId : EntityId<TEntityId>, new()
        {
            return validator.RuleFor(expression)
                .NotNullEntityId()
                .DependentRules(
                    () =>
                    {
                        validator.RuleFor(expression).NotTransientEntityId();
                    }
                );
        }

        public static IRuleBuilderOptions<T, TEntityId> NotNullEntityId<T, TEntityId>(this IRuleBuilderInitial<T, TEntityId> validator)
        where TEntityId : EntityId<TEntityId>, new()
        {
            return validator.NotNull().WithMessage($"The ID {typeof(TEntityId).Name} is required.");
        }

        public static IRuleBuilderOptions<T, TEntityId> NotTransientEntityId<T, TEntityId>(this IRuleBuilderInitial<T, TEntityId> validator)
        where TEntityId : EntityId<TEntityId>, new()
        {
            return validator.Must(entityId => !entityId.IsTransient()).WithMessage($"The ID {typeof(TEntityId).Name} is an invalid format.");
        }

        public static IRuleBuilderOptions<T, TEnumeration> Enumeration<T, TEnumeration>(
            this AbstractValidator<T> validator,
            Expression<Func<T, TEnumeration>> expression
        )
        where TEnumeration : Enumeration
        {
            return validator.RuleFor(expression)
                .NotNullEnumeration()
                .DependentRules(
                    () =>
                    {
                        validator.RuleFor(expression).IsInEnumeration();
                    }
                );
        }

        public static IRuleBuilderOptions<T, TEnumeration> NotNullEnumeration<T, TEnumeration>(this IRuleBuilderInitial<T, TEnumeration> validator)
        where TEnumeration : Enumeration
        {
            return validator.NotNull().WithMessage($"The enumeration {typeof(TEnumeration).Name} is required.");
        }

        public static IRuleBuilderOptions<T, TEnumeration> IsInEnumeration<T, TEnumeration>(this IRuleBuilderInitial<T, TEnumeration> validator)
        where TEnumeration : Enumeration
        {
            return validator.Must(Domain.Aggregate.Enumeration.HasEnumeration)
                .WithMessage(
                    $"The enumeration {typeof(TEnumeration).Name} is invalid. These are valid enumeration names: [{string.Join(", ", Domain.Aggregate.Enumeration.GetAll<TEnumeration>())}]."
                );
        }
    }
}

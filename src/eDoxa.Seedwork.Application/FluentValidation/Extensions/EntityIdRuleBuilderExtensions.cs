// Filename: EntityIdRuleBuilderExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;

using FluentValidation;

namespace eDoxa.Seedwork.Application.FluentValidation.Extensions
{
    public static class EntityIdRuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, TEntityId> NotNull<T, TEntityId>(this IRuleBuilder<T, TEntityId> validator)
        where TEntityId : EntityId<TEntityId>, new()
        {
            return DefaultValidatorExtensions.NotNull(validator).WithMessage($"The ID {typeof(TEntityId).Name} is required.");
        }

        public static IRuleBuilderOptions<T, TEntityId> NotEmpty<T, TEntityId>(this IRuleBuilder<T, TEntityId> validator)
        where TEntityId : EntityId<TEntityId>, new()
        {
            return validator.Must(entityId => !entityId.IsTransient()).WithMessage($"The ID {typeof(TEntityId).Name} is an invalid format.");
        }
    }
}

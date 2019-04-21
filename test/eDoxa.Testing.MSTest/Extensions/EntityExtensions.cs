// Filename: EntityExtensions.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Testing.MSTest.Extensions
{
    public static class EntityExtensions
    {
        public static void SetEntityIdProperty<TEntityId>(this Entity<TEntityId> entity, [CanBeNull] TEntityId entityId)
        where TEntityId : EntityId<TEntityId>, new()
        {
            try
            {
                entity.GetType().GetProperty(nameof(entity.Id)).SetValue(entity, entityId);
            }
            catch (Exception exception)
            {
                throw exception.InnerException;
            }
        }
    }
}
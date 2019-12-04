// Filename: DomainValidationMetadata.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

namespace eDoxa.Seedwork.Domain
{
    public sealed class DomainValidationMetadata : Dictionary<string, object>
    {
        public void AddEntity<TEntity>(TEntity entity)
        where TEntity : IEntity
        {
            this.Add(nameof(Entity), entity);
        }

        public TEntity GetEntity<TEntity>()
        {
            if (this.TryGetValue(nameof(Entity), out var entity))
            {
                return (TEntity) entity;
            }

            throw new NullReferenceException("The response metadata has not been added to the validation result.");
        }
    }
}

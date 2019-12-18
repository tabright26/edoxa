// Filename: GuidExtensions.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.Domain.Extensions
{
    public static class GuidExtensions
    {
        public static TEntityId From<TEntityId>(this Guid entityId)
        where TEntityId : EntityId<TEntityId>, new()
        {
            return EntityId<TEntityId>.FromGuid(entityId);
        }
    }
}

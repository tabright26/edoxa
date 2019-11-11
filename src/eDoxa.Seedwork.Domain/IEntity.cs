// Filename: IEntity.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

namespace eDoxa.Seedwork.Domain
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void AddDomainEvent(IDomainEvent domainEvent);

        void ClearDomainEvents();
    }

    public interface IEntity<TEntityId> : IEntity
    where TEntityId : EntityId<TEntityId>, new()
    {
        TEntityId Id { get; }

        void SetEntityId(TEntityId entityId);
    }
}

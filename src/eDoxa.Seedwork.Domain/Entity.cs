// Filename: Entity.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

namespace eDoxa.Seedwork.Domain
{
    public abstract class Entity<TEntityId> : IEntity
    where TEntityId : EntityId<TEntityId>, new()
    {
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        private int? _requestedHashCode;

        protected Entity()
        {
            Id = new TEntityId();
        }

        public virtual TEntityId Id { get; private set; }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public bool IsTransient()
        {
            return Id.IsTransient();
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Entity<TEntityId> entity))
            {
                return false;
            }

            if (ReferenceEquals(this, entity))
            {
                return true;
            }

            if (this.GetType() != entity.GetType())
            {
                return false;
            }

            if (this.IsTransient() || entity.IsTransient())
            {
                return false;
            }

            return Id == entity.Id;
        }

        public override int GetHashCode()
        {
            if (!this.IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = Id.GetHashCode() ^ 31;
                }

                // XOR for random distribution. See:
                // https://blogs.msdn.microsoft.com/ericlippert/2011/02/28/guidelines-and-rules-for-gethashcode/
                return _requestedHashCode.Value;
            }

            return base.GetHashCode();
        }

        public static bool operator ==(Entity<TEntityId>? left, Entity<TEntityId>? right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(Entity<TEntityId>? left, Entity<TEntityId>? right)
        {
            return !(left == right);
        }

        public void SetEntityId(Guid id)
        {
            var entityId = EntityId<TEntityId>.FromGuid(id);

            this.SetEntityId(entityId);
        }

        public void SetEntityId(TEntityId entityId)
        {
            if (entityId.IsTransient())
            {
                throw new ArgumentException(nameof(entityId));
            }

            Id = entityId;
        }
    }
}

// Filename: Entity.cs
// Date Created: 2019-03-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract partial class Entity<TEntityId> : BaseObject
    where TEntityId : EntityId<TEntityId>, new()
    {
        private const int HashMultiplier = 31;

        private int? _cachedHashCode;
        private TEntityId _id;

        protected Entity()
        {
            _id = new TEntityId();
        }

        public TEntityId Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value ?? throw new ArgumentNullException(nameof(Id));
            }
        }

        public static bool operator ==(Entity<TEntityId> left, Entity<TEntityId> right)
        {
            return EqualityComparer<Entity<TEntityId>>.Default.Equals(left, right);
        }

        public static bool operator !=(Entity<TEntityId> left, Entity<TEntityId> right)
        {
            return !(left == right);
        }

        public override bool Equals([CanBeNull] object obj)
        {
            var other = obj as Entity<TEntityId>;

            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            if (this.IsTransient() || other.IsTransient())
            {
                return false;
            }

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            if (_cachedHashCode.HasValue)
            {
                return _cachedHashCode.Value;
            }

            if (this.IsTransient())
            {
                _cachedHashCode = base.GetHashCode();
            }
            else
            {
                unchecked
                {
                    var hashCode = this.GetType().GetHashCode();

                    _cachedHashCode = hashCode * HashMultiplier ^ Id.GetHashCode();
                }
            }

            return _cachedHashCode.Value;
        }

        public bool IsTransient()
        {
            return Id == null || Id.IsTransient();
        }

        protected override PropertyInfo[] TypeSignatureProperties()
        {
            return Array.Empty<PropertyInfo>();
        }
    }

    public abstract partial class Entity<TEntityId> : IEntity
    where TEntityId : EntityId<TEntityId>, new()
    {
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public IReadOnlyCollection<IDomainEvent> DomainEvents
        {
            get
            {
                return _domainEvents;
            }
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            _domainEvents.Add(domainEvent);
        }
    }
}
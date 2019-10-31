// Filename: Credential.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Games.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate
{
    public sealed class Credential : IEquatable<Credential?>, IEntity, IAggregateRoot
    {
        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

        public Credential(
            UserId userId,
            Game game,
            PlayerId playerId,
            IDateTimeProvider provider
        ) : this()
        {
            UserId = userId;
            Game = game;
            PlayerId = playerId;
            Timestamp = provider.DateTime;
            this.AddDomainEvent(new CredentialCreatedDomainEvent(this));
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private Credential()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            // Required by EF Core.
        }

        public UserId UserId { get; private set; }

        public Game Game { get; private set; }

        public PlayerId PlayerId { get; private set; }

        public DateTime Timestamp { get; private set; }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public bool Equals(Credential? other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(UserId, other.UserId) && Equals(Game, other.Game) && Equals(PlayerId, other.PlayerId);
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Credential other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = UserId != null ? UserId.GetHashCode() : 0;

                hashCode = (hashCode * 397) ^ (Game != null ? Game.GetHashCode() : 0);

                hashCode = (hashCode * 397) ^ (PlayerId != null ? PlayerId.GetHashCode() : 0);

                return hashCode;
            }
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void Delete()
        {
            this.AddDomainEvent(new CredentialDeletedDomainEvent(this));
        }
    }
}

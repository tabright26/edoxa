// Filename: CredentialDeletedDomainEvent.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Games.Domain.DomainEvents
{
    public sealed class CredentialDeletedDomainEvent : IDomainEvent
    {
        public CredentialDeletedDomainEvent(Credential credential)
        {
            Credential = credential;
        }

        public Credential Credential { get; }
    }
}

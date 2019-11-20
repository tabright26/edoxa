// Filename: CredentialCreatedDomainEvent.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Games.Domain.DomainEvents
{
    public sealed class CredentialCreatedDomainEvent : IDomainEvent
    {
        public CredentialCreatedDomainEvent(Credential credential)
        {
            Credential = credential;
        }

        public Credential Credential { get; }
    }
}

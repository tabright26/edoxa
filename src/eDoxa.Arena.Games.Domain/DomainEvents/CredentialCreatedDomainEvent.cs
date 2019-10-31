// Filename: CredentialCreatedDomainEvent.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.Domain.AggregateModels.CredentialAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Games.Domain.DomainEvents
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

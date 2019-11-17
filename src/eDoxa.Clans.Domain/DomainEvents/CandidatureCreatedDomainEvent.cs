// Filename: CandidatureCreatedDomainEvent.cs
// Date Created: 2019-10-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Clans.Domain.DomainEvents
{
    public sealed class CandidatureCreatedDomainEvent : IDomainEvent
    {
        public CandidatureCreatedDomainEvent(Candidature candidature)
        {
            Candidature = candidature;
        }

        public Candidature Candidature { get; }
    }
}

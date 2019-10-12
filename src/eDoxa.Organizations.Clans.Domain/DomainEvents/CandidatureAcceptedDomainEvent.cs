// Filename: CandidatureAcceptedDomainEvent.cs
// Date Created: 2019-10-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Organizations.Clans.Domain.DomainEvents
{
    public sealed class CandidatureAcceptedDomainEvent : IDomainEvent
    {
        public CandidatureAcceptedDomainEvent(Candidature candidature)
        {
            ClanId = candidature.ClanId;
            Candidature = candidature;
        }

        public ClanId ClanId { get; }

        public IMemberInfo Candidature { get; }
    }
}

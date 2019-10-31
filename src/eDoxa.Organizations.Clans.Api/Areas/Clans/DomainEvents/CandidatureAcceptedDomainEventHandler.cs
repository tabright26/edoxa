// Filename: CandidatureAcceptedDomainEventHandler.cs
// Date Created: 2019-10-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Organizations.Clans.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Organizations.Clans.Api.Areas.Clans.DomainEvents
{
    public sealed class CandidatureAcceptedDomainEventHandler : IDomainEventHandler<CandidatureAcceptedDomainEvent>
    {
        private readonly IClanService _clanService;

        public CandidatureAcceptedDomainEventHandler(IClanService clanService)
        {
            _clanService = clanService;
        }

        public async Task Handle(CandidatureAcceptedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _clanService.AddMemberToClanAsync(domainEvent.ClanId, domainEvent.Candidature);
        }
    }
}
